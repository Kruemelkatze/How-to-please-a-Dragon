using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : SceneSingleton<Pile>
{
    public float SmoothTime = 0.3f;
    private Vector3 _moveTargetDefault;
    private Vector3 _velocity = Vector3.zero;

    public float Level = 0;
    public GameObject MoveTarget;

    public float MaxHeightOffset = 10;
    public float MinHeightOffset = 0;

    // Use this for initialization
    void Start()
    {
        _moveTargetDefault = MoveTarget.transform.position;
        //Instantly move
        UpdateMoveTarget(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMoveTarget();
    }

    private void UpdateMoveTarget(bool instantly = false)
    {
        var newMoveTargetPos = new Vector3(
            MoveTarget.transform.position.x,
            _moveTargetDefault.y + Level / 100f * MaxHeightOffset + MinHeightOffset,
            MoveTarget.transform.position.z
        );

        if (instantly)
        {
            MoveTarget.transform.position = newMoveTargetPos;
        }
        else
        {
            MoveTarget.transform.position =
                Vector3.SmoothDamp(MoveTarget.transform.position, newMoveTargetPos, ref _velocity, SmoothTime);
        }
    }

    public void Add(float amount)
    {
        float realSum = Level + amount;
        Level = Math.Min(100, realSum);
        
        if (realSum > 100)
        {
            GameManager.Instance.PileFull();
        }
    }

    public float Subtract(float amount)
    {
        if (amount > Level)
        {
            amount = Level;
        }

        Level -= amount;
        
        // Returns the amount that was subtracted
        return amount;
    }
}