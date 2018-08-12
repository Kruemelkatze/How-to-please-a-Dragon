using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DragonsPersonality : MonoBehaviour
{
    public float Rage = 50;
    public float RageDecreasePerSecond = 0.5f;

    public DragonMood Mood
    {
        get
        {
            if (Rage < 30f)
                return DragonMood.Friendly;
            else if (Rage > 80)
                return DragonMood.Raged;
            else
                return DragonMood.Normal;
        }
    }

    public TextMeshProUGUI RageDebugText;

    private bool _debug;

    // Use this for initialization
    void Start()
    {
        _debug = Application.isEditor;

        if (!_debug && RageDebugText != null)
        {
            RageDebugText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.DecreaseRage)
        {
            Rage = Math.Max(0, Rage - RageDecreasePerSecond * Time.deltaTime);

            if (_debug && RageDebugText != null)
            {
                RageDebugText.text = $"Rage: {Rage}";
            }
        }
    }

    public void AddRage(float amount)
    {
        Rage = Math.Min(100, Rage + amount);
        if (Rage >= 100)
        {
            GameManager.Instance.DragonRaged();
        }
    }
}

public enum DragonMood
{
    Friendly,
    Normal,
    Raged
}