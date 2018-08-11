using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SceneSingleton<GameManager>
{
    public delegate void VoidEvent();

    public event VoidEvent OnGameEnd;

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

    void EndGame()
    {
        Debug.Log("Game Ended");
        CanShovel = false;
        DragonCanCarry = false;

        OnGameEnd?.Invoke();
    }

    public void PileFull()
    {
        Debug.Log("PILE FULL.");
        EndGame();
    }
}