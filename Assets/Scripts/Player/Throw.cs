﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Throw : MonoBehaviour
{
    public float AnimationTime;
    public Vector3 StartPosition;
    public Vector3 TargetPosition;

    public float Height = 5;
    public float CompletionTime = 1;

    private Rigidbody2D _rigidbody2D;

    public int CoindropSoundCount = 3;
    public float RandomAngularVelocity = 5;

    public Vector3 TargetTrajectoryOffset;

    public int Amount;
    public ShelfDisplay TargetShelf;

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        var velocity = Random.Range(-RandomAngularVelocity, RandomAngularVelocity) / 2;
        _rigidbody2D.angularVelocity = velocity + Math.Sign(velocity) * RandomAngularVelocity / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetPosition != StartPosition)
        {
            AnimationTime += Time.deltaTime;
            AnimationTime = Math.Min(AnimationTime, CompletionTime);

            _rigidbody2D.position =
                MathParabola.Parabola(StartPosition, TargetPosition + TargetTrajectoryOffset, Height,
                    AnimationTime / CompletionTime);
        }
    }

    public void SetTrajectory(Vector3 start, Vector3 target)
    {
        transform.position = start;
        StartPosition = start;
        TargetPosition = target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shelf"))
        {
            var backAmount = ShelfManager.Instance.Add(Amount, TargetShelf);
            if (backAmount > 0)
                Pile.Instance.Add(backAmount);
            
            Highscore.Instance.Add(Amount - backAmount);

            // Pick a random sound for the coins dropping into the shelf
            String coindropSound = "coindrop" + UnityEngine.Random.Range(1, CoindropSoundCount + 1);
            AudioControl.Instance.PlaySound(coindropSound, 0.2f);
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(StartPosition, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(TargetPosition, 0.5f);
    }
}