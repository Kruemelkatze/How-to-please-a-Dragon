using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DeathScreen : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TopScoreText;

    private void Start()
    {
        if (ScoreText != null)
        {
            ScoreText.text = Highscore.LatestScore.ToString();
        }

        if (TopScoreText != null)
        {
            TopScoreText.text = Highscore.TopHighscore.ToString();
        }
    }
}