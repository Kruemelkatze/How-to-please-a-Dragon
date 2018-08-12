using System;
using UnityEngine;

public class ShovelScript : SceneSingleton<ShovelScript>
{
    public int ShovelAmount = 0;

    public SpriteRenderer Shovel;
    
    public int ShovelUpgrade = 0;
    public int[] AmountMap = new[] {50, 100, 200};
    
    // Use this for initialization
    void Start()
    {
        setAmount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpgradeShovel(int level)
    {
        level = Math.Max(level, ShovelUpgrade);
        ShovelUpgrade = level > 2 ? 2 : level;
        setAmount();
    }

    void DowngradeShovel(int level)
    {
        level = Math.Min(level, ShovelUpgrade);
        ShovelUpgrade = level < 0 ? 0 : level;
        setAmount();
    }

    private void setAmount()
    {
        ShovelAmount = AmountMap[ShovelUpgrade];
    }
}