using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SceneSingleton<GameManager>
{
    public bool CanShovel = true;
    public bool DragonCanCarry = true;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnGameEnd()
    {
        CanShovel = false;
        DragonCanCarry = false;
    }

    public void PileFull()
    {
        Debug.Log("PILE FULL.");
        OnGameEnd();
    }
}