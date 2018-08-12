using System.Collections;
using UnityEngine;

public class LootManager : SceneSingleton<LootManager>
{
    public Animation PanelAnimation;

    public bool StopTimeOnModal;

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
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                DeclineLoot();
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
        yield return new WaitForSeconds(delay);
        PanelAnimation.gameObject.SetActive(true);
        PanelAnimation.Play("LootModal_in");
        var animLength = PanelAnimation.GetClip("LootModal_in").length;

        StartCoroutine(OnModalFocus(animLength));
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
        var animLength = PanelAnimation.GetClip("LootModal_out").length;
        GameManager.Instance.ModalFocus(false);
    }
}