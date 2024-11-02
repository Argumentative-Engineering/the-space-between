using DG.Tweening;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public bool IsEquipped { get; set; }
    Vector3 _startPos;

    public virtual void Equip(bool isEquipped)
    {
        if (!isEquipped) Dequip();
        if (!IsEquipped)
            transform.DOMove(_startPos, 2);

        IsEquipped = isEquipped;
    }

    public virtual void Dequip()
    {
        IsEquipped = false;
        transform.DOMove(_startPos + Vector3.down, 2).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}