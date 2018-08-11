using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Shelf;
using UnityEngine;

public class ShelfManager : SceneSingleton<ShelfManager>
{
    private ShelfDisplay Selected;
    private int SelectedIndex;

    public List<ShelfDisplay> Shelves;

    // Use this for initialization
    void Start()
    {
        SelectedIndex = 0;
        if (Shelves.Count > 0)
        {
            Selected = Shelves[SelectedIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int Add(int amount)
    {
        var sum = Selected.CurrentAmount + amount;
        Selected.CurrentAmount = Math.Min(Selected.Shelf.TotalAmount, sum);

        if (sum < Selected.Shelf.TotalAmount)
            return 0;

        return sum - Selected.Shelf.TotalAmount;
    }

    public void Subtract(int amount)
    {
        Selected.CurrentAmount -= amount;

        if (Selected.CurrentAmount < 0)
        {
            Selected.CurrentAmount = 0;
        }
    }

    public void SelectLeft()
    {
        if (SelectedIndex > 0)
        {
            Selected = Shelves[--SelectedIndex];
        }
    }

    public void SelectRight()
    {
        if (SelectedIndex < Shelves.Count - 1)
        {
            Selected = Shelves[++SelectedIndex];
        }
    }
}