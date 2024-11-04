using DG.Tweening;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public bool IsEquipped { get; set; }
    Vector3 _startPos;

    private void Awake()
    {
        _startPos = transform.localPosition;
    }

    public virtual void Equip(bool isEquipped, bool forceEquip = false)
    {
        print("Equip");
        if (PlayerSettings.Instance.IsFrozen && !forceEquip) return;
        transform.DOKill(true);
        if (!isEquipped)
        {
            Dequip();
            return;
        }

        gameObject.SetActive(isEquipped);

        if (!IsEquipped)
            transform.DOLocalMove(_startPos, 1);

        IsEquipped = isEquipped;
    }

    public virtual void Dequip()
    {
        if (!IsEquipped) return;
        transform.DOKill(true);
        IsEquipped = false;
        transform.DOLocalMove(_startPos + Vector3.down, 1).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}