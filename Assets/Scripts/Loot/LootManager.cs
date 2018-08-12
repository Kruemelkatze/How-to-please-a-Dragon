using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : SceneSingleton<LootManager>
{
    public Animation PanelAnimation;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowModal()
    {
        PanelAnimation.gameObject.SetActive(true);
        PanelAnimation.Play("LootModal_in");
        Debug.Log(PanelAnimation.GetClip("LootModal_in").length);
    }

    public void HideModal()
    {
        PanelAnimation.Play("LootModal_out");
        Debug.Log(PanelAnimation.GetClip("LootModal_out").length);
    }
}