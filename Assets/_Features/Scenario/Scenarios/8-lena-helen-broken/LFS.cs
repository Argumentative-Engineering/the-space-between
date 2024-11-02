using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFS : DialogueInteractable
{
    private void Start()
    {
        Tooltip = "Vitality Module";
    }

    public override bool TryInteract()
    {
        base.TryInteract();
        EventManager.Instance.BroadcastEvent("got-lfs");
        PlayerOxygen.Instance.CurrentOxygen = 100;
        Destroy(gameObject);
        return false;
    }
}
