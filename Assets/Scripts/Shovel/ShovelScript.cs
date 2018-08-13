using System;
using UnityEditor;
using UnityEngine;

public class ShovelScript : SceneSingleton<ShovelScript>
{
    public int ShovelAmount = 0;

    public SpriteRenderer Shovel;
    
    public int ShovelUpgrade = 0;
    public int[] AmountMap = new[] {50, 100, 200};
    private Sprite[] SpriteMap = new Sprite[3];
    
    // Use this for initialization
    void Start()
    {
        setAmount();
        SpriteMap[0] = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Player/char_shovel_up.png");
        SpriteMap[1] = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Player/char_shovel_up_2.png");
        SpriteMap[2] = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Player/char_shovel_up_3.png");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeShovel(int level)
    {
        level = Math.Max(level, ShovelUpgrade);
        ShovelUpgrade = level > 2 ? 2 : level;
        setAmount();
    }

    public void DowngradeShovel(int level)
    {
        level = Math.Min(level, ShovelUpgrade);
        ShovelUpgrade = level < 0 ? 0 : level;
        setAmount();
    }

    private void setAmount()
    {
        ShovelAmount = AmountMap[ShovelUpgrade];
        Shovel.sprite = SpriteMap[ShovelUpgrade];
    }
}