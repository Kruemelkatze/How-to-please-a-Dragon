using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class DragonsPersonality : SceneSingleton<DragonsPersonality>
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
                return DragonMood.Angry;
            else
                return DragonMood.Normal;
        }
    }

    public TextMeshProUGUI RageDebugText;

    private bool _debug;

    public TextAsset TextFile;

    public List<TextDefinition> MoodTexts;

    void Awake()
    {
        SetInstance();
        LoadTexts();
    }

    public void LoadTexts()
    {
        if (TextFile != null)
        {
            var obj = JsonUtility.FromJson<TextFileContent>(TextFile.text);

            MoodTexts = new List<TextDefinition>()
            {
                new TextDefinition() {Mood = DragonMood.Friendly, Texts = obj.Friendly},
                new TextDefinition() {Mood = DragonMood.Normal, Texts = obj.Normal},
                new TextDefinition() {Mood = DragonMood.Angry, Texts = obj.Angry},
            };
        }
    }

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

    public string GetRandomText()
    {
        var texts = MoodTexts.FirstOrDefault(def => def.Mood == Mood);
        if (texts == null)
        {
            return "The dragon ran out of words to say.";
        }

        int i = Random.Range(0, texts.Texts.Length);
        return texts.Texts[i];
    }
}

public enum DragonMood
{
    Friendly,
    Normal,
    Angry
}

[System.Serializable]
class TextFileContent
{
    public string[] Friendly;
    public string[] Normal;
    public string[] Angry;
}

[System.Serializable]
public class TextDefinition
{
    [ReadOnly] public DragonMood Mood;

    public string[] Texts;
}