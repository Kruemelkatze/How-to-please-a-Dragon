using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject ThrowStuff;
    public GameObject Shovel;
    public ParticleSystem ParticleSystem;
    public int shovelSoundCount = 4;
    public Animator Animator;
    public Vector3 OffsetThrow;

    void Start()
    {
        Shovel = GameObject.Instantiate(Shovel);
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
        var ShovelAmount = ShovelScript.Instance.ShovelAmount;
        var addAmount = Pile.Instance.Subtract(ShovelAmount);
        
        if (addAmount > 0)
        {
            var backAmount = ShelfManager.Instance.Add(addAmount);
            if (backAmount > 0)
                Pile.Instance.Add(backAmount);

            Animator.SetTrigger("PlayerShoveling");
            PlayShovelSound();
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

            throwScript.SetTrajectory((transform.position + OffsetThrow), shelfPosition);
        } else
        {
            AudioControl.Instance.PlaySound("shovel_ground",0.3f);
        }
    }

    private void PlayShovelSound()
    {
        // Number of available shovel sounds
        String shovelSound = "shovel" + UnityEngine.Random.Range(1, shovelSoundCount + 1); 
        AudioControl.Instance.PlaySound(shovelSound, 0.2f);
    }
}