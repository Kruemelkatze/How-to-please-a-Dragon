using System.Collections;
using TMPro;
using UnityEngine;

public class LootManager : SceneSingleton<LootManager>
{
    public Animation PanelAnimation;

    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemDescriptionText;
    public TextMeshProUGUI RageIncreaseText;

    public bool StopTimeOnModal;

    public string[] RageIncreaseTexts = new string[3];
    public Color[] RageIncreaseColors = new Color[3];
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
        Debug.Log("Accepted Loot " + JsonUtility.ToJson(Pile.Instance.ContainedLoot));
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
}