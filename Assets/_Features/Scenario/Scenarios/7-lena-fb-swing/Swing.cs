using UnityEngine;

public class Swing : GameInteractable
{
    int _pushCount;
    [SerializeField]
    Rigidbody _rb;

    public override bool TryInteract()
    {
        _rb.AddForce((_pushCount + 2) * transform.up, ForceMode.Impulse);
        _pushCount = Mathf.Min(5, _pushCount + 1);
        EventManager.Instance.BroadcastEvent("push-swing", _pushCount);
        return base.TryInteract();
    }
}