using System;
using UnityEditor;
using UnityEngine;

public class ShovelScript : SceneSingleton<ShovelScript>
{
    public int ShovelAmount = 30;

    public SpriteRenderer Shovel;
    
    public int ShovelUpgrade = 0;
    public int[] AmountMap = new[] {30, 70, 150};
    
    // Use this for initialization
    void Start()
    {
        setAmount();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int UpgradeShovel(int level)
    {
        level = Math.Max(level, ShovelUpgrade);
        ShovelUpgrade = level > 2 ? 2 : level;
        setAmount();

        return ShovelUpgrade;
    }

    public int DowngradeShovel(int level)
    {
        level = Math.Min(level, ShovelUpgrade);
        ShovelUpgrade = level < 0 ? 0 : level;
        setAmount();

        return ShovelUpgrade;
    }

    private void setAmount()
    {
        ShovelAmount = AmountMap[ShovelUpgrade];
    }
}