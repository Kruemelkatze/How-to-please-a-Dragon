using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRandomMovement : MonoBehaviour
{
    public Vector3 CurrentTarget;
    private Vector3 _velocity;
    public float SmoothTime = 0.3f;

    public Vector2 RandomRange = new Vector2(0.5f, 0.5f);
    public float DistanceToSwitch = 0.05f;

    public bool Enabled = true;

    // Use this for initialization
    void Start()
    {
        CurrentTarget = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled)
        {
            if (Vector3.Distance(transform.position, CurrentTarget) < DistanceToSwitch)
            {
                Vector2 random = Random.insideUnitCircle * RandomRange;
                CurrentTarget = new Vector3(random.x, random.y, CurrentTarget.z);
            }

            transform.position = Vector3.SmoothDamp(transform.position, CurrentTarget, ref _velocity, SmoothTime);
        }
    }
}