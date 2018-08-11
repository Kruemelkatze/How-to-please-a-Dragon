using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ShelfManager.Instance.SelectLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ShelfManager.Instance.SelectRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            int removeAmount = 100;

            Pile.Instance.Subtract(removeAmount);
            var addAmount = ShelfManager.Instance.Add(removeAmount);
            if (addAmount > 0)
            {
                Pile.Instance.Add(addAmount);
            }
        }
    }
}