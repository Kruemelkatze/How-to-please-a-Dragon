using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SceneSingleton<GameManager>
{
    public delegate void GameEndEvent(GameEndReason reason);

    public event GameEndEvent OnGameEnd;

    public bool CanShovel = true;
    public bool DragonCanCarry = true;
    public bool DecreaseRage = true;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void EndGame(GameEndReason reason)
    {
        Debug.Log("Game Ended because of " + reason);
        CanShovel = false;
        DragonCanCarry = false;
        DecreaseRage = false;
        
        OnGameEnd?.Invoke(reason);
    }

    public void PileFull()
    {
        Debug.Log("PILE FULL.");
        EndGame(GameEndReason.PileFull);
    }

    public void DragonRaged()
    {
        Debug.Log("DRAGON RAGED.");
        EndGame(GameEndReason.DragonRaged);
    }
}

public enum GameEndReason
{
    PileFull,
    DragonRaged
}