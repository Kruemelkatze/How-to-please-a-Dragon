using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int ShovelAmount = 100;
    public GameObject ThrowStuff;
    public ParticleSystem ParticleSystem;

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
        var addAmount = Pile.Instance.Subtract(ShovelAmount);
        if (addAmount > 0)
        {
            var backAmount = ShelfManager.Instance.Add(addAmount);
            if (backAmount > 0)
                Pile.Instance.Add(backAmount);

            var burst = ParticleSystem.emission.GetBurst(0);
            burst.minCount = (short) (ShovelAmount / 50);
            burst.maxCount = (short) (ShovelAmount / 20);
            ParticleSystem.emission.SetBurst(0, burst);
            ParticleSystem.Play();


            // Instantiate Throw object
            if (ThrowStuff == null)
                return;

            var throwStuff = GameObject.Instantiate(ThrowStuff);
            var throwScript = throwStuff.GetComponent<Throw>();

            var shelfTransform = ShelfManager.Instance.Selected.transform;
            var shelfHeight = shelfTransform.gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.y / 2;
            var shelfPosition = shelfTransform.position + Vector3.up * shelfHeight;

            throwScript.SetTrajectory(transform.position, shelfPosition);
        }
    }
}