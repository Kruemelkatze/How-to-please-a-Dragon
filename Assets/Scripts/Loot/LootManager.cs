using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : SceneSingleton<LootManager>
{
    public Animation PanelAnimation;

    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemDescriptionText;
    public TextMeshProUGUI RageIncreaseText;
    public Image RageImage;

    public bool StopTimeOnModal;

    public string[] RageIncreaseTexts = new string[3];
    public Color[] RageIncreaseColors = new Color[3];
    public Sprite[] RageLevelSprites = new Sprite[3];
    public Color[] LevelColors = new Color[5];

    // Use this for initialization
    void Start()
    {
        PanelAnimation.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.DefaultPlayerActionsActive)
        {
            var mood = DragonsPersonality.Instance.Mood;
            RageImage.sprite = RageLevelSprites[(int) mood];
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                AcceptLoot();
                Input.ResetInputAxes();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                DeclineLoot();
                Input.ResetInputAxes();
            }
        }
    }

    public void AcceptLoot()
    {
        var item = Pile.Instance.ContainedLoot;
        Debug.Log("Accepted Loot " + JsonUtility.ToJson(item));
        HandleAcceptedItem(item);

        Pile.Instance.ContainedLoot = null;
        HideModal();
    }

    public void DeclineLoot()
    {
        Debug.Log("Declined Loot");
        Pile.Instance.ContainedLoot = null;
        HideModal();
    }

    public void ShowModal(float delay = 0)
    {
        StartCoroutine(OpenModal(delay));
    }

    private IEnumerator OpenModal(float delay)
    {
        SetLootValues();

        yield return new WaitForSeconds(delay);
        PanelAnimation.gameObject.SetActive(true);
        PanelAnimation.Play("LootModal_in");
        var animLength = PanelAnimation.GetClip("LootModal_in").length;

        StartCoroutine(OnModalFocus(animLength));
    }

    private void SetLootValues()
    {
        var loot = Pile.Instance.ContainedLoot;

        ItemNameText.text = loot.Name;
        ItemNameText.color = LevelColors[loot.Level];
        ItemDescriptionText.text = loot.Description;

        int rageIncreaseLevel = GetRageIncreaseLevel(loot.RageIncrease);
        RageIncreaseText.text = RageIncreaseTexts[rageIncreaseLevel];
        RageIncreaseText.color = RageIncreaseColors[rageIncreaseLevel];
    }

    private int GetRageIncreaseLevel(float rageIncreaseValue)
    {
        if (rageIncreaseValue >= 60)
        {
            return 2;
        }
        else if (rageIncreaseValue <= 20)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    private IEnumerator OnModalFocus(float animLength)
    {
        yield return new WaitForSecondsRealtime(animLength / 2);
        GameManager.Instance.ModalFocus(true);

        yield return new WaitForSecondsRealtime(animLength / 2);

        if (StopTimeOnModal)
            Time.timeScale = 0;
    }

    public void HideModal()
    {
        if (StopTimeOnModal)
            Time.timeScale = 1;

        PanelAnimation.Play("LootModal_out");

        GameManager.Instance.ModalFocus(false);
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~ UPGRADING ~~~~~~~~~~~~~~~~~~~~~~~~
    private void HandleAcceptedItem(ItemDefinition item)
    {
        switch (item.ItemType)
        {
            case ItemType.ShelfUpgrade:
                var shelfIndex = GetUpgradeIndex(item);
                if (shelfIndex >= 0)
                {
                    ShelfManager.Instance.UpgradeShelf(shelfIndex, item.Level);
                    AudioControl.Instance.PlaySound("item_upgrade", 0.4f);
                }
                else
                {
                    Highscore.Instance.Add((int) (item.RageIncrease * 20));
                }

                break;
            case ItemType.ShovelUpgrade:
                if (PlayerScript.Instance.Shovel.ShovelUpgrade < item.Level)
                {
                    PlayerScript.Instance.UpgradeShovel(item.Level);
                    AudioControl.Instance.PlaySound("item_upgrade", 0.4f);
                }
                else
                {
                    Highscore.Instance.Add((int) (item.RageIncrease * 20));
                }

                break;
            case ItemType.Misc:
                Highscore.Instance.Add((int) (item.RageIncrease * 20));
                break;
        }

        DragonsPersonality.Instance.AddRage(item.RageIncrease);
    }

    private int GetUpgradeIndex(ItemDefinition item)
    {
        var manager = ShelfManager.Instance;
        // If current is applicable
        if (manager.Selected.Shelf.Level < item.Level)
        {
            return manager.SelectedIndex;
        }

        // Else: Get one of the shelves
        for (int i = 0; i < manager.Shelves.Count; i++)
        {
            var j = (manager.SelectedIndex + i) % manager.Shelves.Count;
            var shelfDisplay = manager.Shelves[i];
            if (shelfDisplay.Shelf.Level < item.Level)
            {
                return i;
            }
        }

        // None fits? -1 for highscore
        return -1;
    }
}