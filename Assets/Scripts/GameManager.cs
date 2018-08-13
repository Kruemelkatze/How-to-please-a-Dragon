using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SceneSingleton<GameManager>
{
    public delegate void GameEndEvent(GameEndReason reason);

    public event GameEndEvent OnGameEnd;

    public bool CanShovel = true;
    public bool DragonCanCarry = true;
    public bool DecreaseRage = true;
    public bool DefaultPlayerActionsActive = true;

    public int PileDeathSceneIndex = -1;
    public int DragonDeathSceneIndex = -1;

    private void Awake()
    {
        SetInstance();
        Cursor.visible = false;
    }

    // Use this for initialization
    void Start()
    {
        AudioControl.Instance.PlayMusic("background_game",0.15f);
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

        switch (reason)
        {
            case GameEndReason.DragonRaged:
                SceneTransition.TransitionToScene(DragonDeathSceneIndex);
                break;
            case GameEndReason.PileFull:
                SceneTransition.TransitionToScene(PileDeathSceneIndex);
                break;
        }
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

    public void ModalFocus(bool focus)
    {
        DefaultPlayerActionsActive = !focus;
    }
}

public enum GameEndReason
{
    PileFull,
    DragonRaged
}