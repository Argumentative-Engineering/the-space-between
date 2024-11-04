using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Buttons : GameInteractable
{
    public int keypadNum = 1;
    private Vector3 originalScale;

    private void Start()
    {
        Tooltip = keypadNum.ToString();
        originalScale = transform.localScale;
    }

    public override bool TryInteract()
    {
        transform.DOScale(originalScale * 0.9f, 0.1f).OnComplete(() => transform.DOScale(originalScale, 0.01f));
        OnInteract?.Invoke();
        return false;
    }
}
