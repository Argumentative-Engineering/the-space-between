using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPipe : GameInteractable
{
    [SerializeField] bool _isProperPipe;

    public override bool TryInteract()
    {
        EventManager.Instance.BroadcastEvent(EventDefinitions.CheckPipe, _isProperPipe, gameObject);
        return base.TryInteract();
    }
}
