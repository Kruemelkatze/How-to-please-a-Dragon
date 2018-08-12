using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : SceneSingleton<Pile>
{
    public SpriteMask PileMask;
    private bool _deathByPile = false;

    public float SmoothTime = 0.3f;
    private Vector3 _moveTargetDefault;
    private Vector3 _velocity = Vector3.zero;

    public int MaxLevel = 10000;
    public int Level = 2000;

    public float FillingPercentage => (float) Level / MaxLevel;
    public GameObject MoveTarget;

    public float MaxHeightOffset = 10;
    public float MinHeightOffset = 0;

    public ItemDefinition ContainedLoot;

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
        IncreasePile();
    }

    private void UpdateMoveTarget(bool instantly = false)
    {
        if (_deathByPile)
        {
            return;
        }

        var newMoveTargetPos = new Vector3(
            MoveTarget.transform.position.x,
            _moveTargetDefault.y + FillingPercentage * MaxHeightOffset + MinHeightOffset,
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

    public void Add(int amount)
    {
        int realSum = Level + amount;
        Level = Math.Min(MaxLevel, realSum);

        if (realSum > MaxLevel)
        {
            _deathByPile = true;
        }
    }

    public void IncreasePile()
    {
        
        var scale = MoveTarget.transform.localScale;
        
        if (_deathByPile && scale.x < 5)
        {
            var newScale = new Vector3(0.25F, 0.25F, 0);
            float speed = 2.0F;
            PileMask.transform.localScale += newScale;
            MoveTarget.transform.localScale += newScale * speed * Time.deltaTime;
            MoveTarget.transform.position += new Vector3(0, 1.4F, 0) * speed * Time.deltaTime;

            if (scale.x < 3 && MoveTarget.transform.localScale.x >= 3)
            {
                GameManager.Instance.PileFull();
            }
        }
    }

    public int Subtract(int amount)
    {
        if (amount > Level)
        {
            amount = Level;
        }

        Level -= amount;

        // Returns the amount that was subtracted
        return amount;
    }

    public void SetLoot(ItemDefinition loot)
    {
        ContainedLoot = loot;
        AudioControl.Instance.PlaySound("loot");
        Debug.Log("Loot added to Pile");
    }
}