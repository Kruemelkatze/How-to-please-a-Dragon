using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int ShovelAmount = 100;
    public GameObject ThrowStuff;

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
            Throw();
        }
    }

    private void Throw()
    {
        var addAmount = ShelfManager.Instance.Add(Pile.Instance.Subtract(ShovelAmount));
        if (addAmount > 0)
        {
            Pile.Instance.Add(addAmount);
        }

        // Instantiate Throw object
        if (ThrowStuff == null)
            return;
        
        var throwStuff = GameObject.Instantiate(ThrowStuff);
        var throwScript = throwStuff.GetComponent<Throw>();

        var shelfTransform = ShelfManager.Instance.Selected.transform;
        var shelfHeight = shelfTransform.gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.y /2;
        var shelfPosition = shelfTransform.position + Vector3.up * shelfHeight;

        throwScript.SetTrajectory(transform.position, shelfPosition);
    }
}