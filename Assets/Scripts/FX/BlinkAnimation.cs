using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAnimation : MonoBehaviour
{
    [SerializeField] private Transform spriteObj;
    [SerializeField] private float maxScale;

    private float animDuration = 0.5f;
    private bool isScaleDown;
    public bool IsScaleDown => isScaleDown;
    public event Action onAnimCompleted;

    Sequence scaleSequence;

    private void OnEnable()
    {
        isScaleDown = false;
        DoScale(0, 0);
    }
    private void OnDisable ()
    {
        isScaleDown = false;
    }
    public void DoAnimation()
    {
        scaleSequence = DOTween.Sequence();
        scaleSequence.Append(DoScale(maxScale, animDuration))
            .AppendCallback(() => isScaleDown = true)
            .Append(DoScale(0, animDuration))
            .AppendCallback(() => onAnimCompleted?.Invoke());
    }
    private Tween DoScale(float scale, float duration)
    {
        return spriteObj.DOScale(scale, duration);
    }
}
