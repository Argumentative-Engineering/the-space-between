using FMODUnity;
using UnityEngine;

public class Swing : GameInteractable
{
    [SerializeField] EventReference _swingSfx;
    int _pushCount;
    [SerializeField]
    Rigidbody _rb;

    public override bool TryInteract()
    {
        _rb.AddForce((_pushCount + 1) * transform.up, ForceMode.Impulse);
        _pushCount = Mathf.Min(5, _pushCount + 1);
        RuntimeManager.PlayOneShotAttached(_swingSfx, gameObject);
        // EventManager.Instance.BroadcastEvent(EventDefinitions.PushSwing, _pushCount);

        return base.TryInteract();
    }
}