using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimmer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public float TargetAlpha;
    public float SmoothTime = 0.3f;

    private float _alpha;

    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var currentAlpha = _spriteRenderer.color.a;
        if (TargetAlpha != 0 && Math.Abs(TargetAlpha - currentAlpha) < 0.01)
        {
            TargetAlpha = 0;
        }

        float newAlpha = Mathf.SmoothDamp(currentAlpha, TargetAlpha, ref _alpha, SmoothTime);

        _spriteRenderer.color = new Color(0, 0, 0, newAlpha);
    }

    public void Dim(float targetAlpha = 0.313f)
    {
        TargetAlpha = targetAlpha;
    }
}