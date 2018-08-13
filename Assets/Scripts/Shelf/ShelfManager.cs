using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Shelf;
using UnityEditor;
using UnityEngine;

public class ShelfManager : SceneSingleton<ShelfManager>
{
    public ShelfDisplay Selected;

    public SpriteRenderer Selection;
    private Vector3 selectionPosition;

    private int _selectedIndex;

    public List<ShelfDisplay> Shelves;

    public int SelectedIndex => _selectedIndex;
    
    // Use this for initialization
    void Start()
    {
        _selectedIndex = 0;
        if (Shelves.Count > 0)
        {
            Selected = Shelves[_selectedIndex];
            Selection.transform.position = Selected.Frame.transform.position + new Vector3(0, -0.24F, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int Add(int amount, ShelfDisplay shelf = null)
    {
        shelf = shelf != null ? shelf : Selected;

        var sum = shelf.CurrentAmount + amount;
        shelf.CurrentAmount = Math.Min(shelf.Shelf.TotalAmount, sum);

        if (sum < shelf.Shelf.TotalAmount)
            return 0;


        // show particel effect for excess of coins
        var tomuch = sum - shelf.Shelf.TotalAmount;
        shelf.TooMuch(tomuch);

        return tomuch;
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
        if (_selectedIndex > 0)
        {
            Selected = Shelves[--_selectedIndex];
            Selection.transform.position = Selected.Frame.transform.position + new Vector3(0, -0.24F, 0);
        }
    }

    public void SelectRight()
    {
        if (_selectedIndex < Shelves.Count - 1)
        {
            Selected = Shelves[++_selectedIndex];
            Selection.transform.position = Selected.Frame.transform.position + new Vector3(0, -0.24F, 0);
        }
    }

    public void UpgradeShelf(int index, int upgrade)
    {
        Shelf shelf = null;
        switch (upgrade)
        {
            case 0:
                shelf = Resources.Load<Shelf>("Shelf/BasicShelf");
                break;
            case 1:
                shelf = Resources.Load<Shelf>("Shelf/Upgrade1Shelf");
                break;
            case 2:
                shelf = Resources.Load<Shelf>("Shelf/Upgrade2Shelf");
                break;
            case 3:
                shelf = Resources.Load<Shelf>("Shelf/Upgrade3Shelf");
                break;
        }

        if (shelf != null)
        {
            Shelves[index].Shelf = shelf;
            if (upgrade == 3)
            {
                Shelves[index].FullyUpgraded.Play();
            }
        }
    }

    public double LevelShelfesFull()
    {
        return Shelves.Average(x => x.FillingPercentage);
     }
}