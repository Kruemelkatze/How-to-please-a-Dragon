using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Highscore : SceneSingleton<Highscore>
{
    public static int TopHighscore;

    public int Score;

    public TextMeshProUGUI HighscoreText;

    // Use this for initialization
    void Start()
    {
        GameManager.Instance.OnGameEnd += OnGameEnd;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameEnd -= OnGameEnd;
    }

    void OnGameEnd(GameEndReason reason)
    {
        TopHighscore = Math.Max(TopHighscore, Score);
    }

    // Update is called once per frame
    void Update()
    {
        if (HighscoreText != null)
        {
            HighscoreText.text = Score.ToString();
        }
    }

    public void Add(int score)
    {
        Score += score;
    }
}