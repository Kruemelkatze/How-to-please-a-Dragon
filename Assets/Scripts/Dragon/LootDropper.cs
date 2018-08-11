using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropper : SceneSingleton<LootDropper>
{
    public GameObject Dropping;
    public int DropAmount = 200;

    public int ScaleBaseValue = 200;

    public bool ScaleWithValue = true;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Drop()
    {
        Drop(DropAmount);
    }

    public void Drop(int amount)
    {
        var drop = GameObject.Instantiate(Dropping, transform);
        var lootDropScript = drop.GetComponent<LootDrop>();
        lootDropScript.Amount = amount;

        if (!ScaleWithValue)
            return;

        var scale = (float) DropAmount / ScaleBaseValue;
        drop.transform.localScale = new Vector3(scale, scale, scale);
    }
}