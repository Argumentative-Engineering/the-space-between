using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine;

public class RemoveTether : GameInteractable
{
    float _startZ;

    private void Start()
    {
        _startZ = transform.localPosition.x;
    }

    public override bool TryInteract()
    {
        transform.DOLocalMoveX(_startZ + -0.08f, 1).OnComplete(() =>
        {
            PlayerSettings.Instance.CanUseTether = true;
            PlayerInventory.Instance.EquipItem(InventoryItems.Tether);
            Destroy(gameObject);
        });
        return false;
    }
}
