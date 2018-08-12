using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public float AnimationTime;
    public Vector3 StartPosition;
    public Vector3 TargetPosition;

    public float Height = 5;
    public float CompletionTime = 1;

    private Rigidbody2D _rigidbody2D;

    public int CoindropSoundCount = 3;

    // Use this for initialization
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetPosition != StartPosition)
        {
            AnimationTime += Time.deltaTime;
            AnimationTime = Math.Min(AnimationTime, CompletionTime);

            _rigidbody2D.position =
                MathParabola.Parabola(StartPosition, TargetPosition, Height, AnimationTime / CompletionTime);
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