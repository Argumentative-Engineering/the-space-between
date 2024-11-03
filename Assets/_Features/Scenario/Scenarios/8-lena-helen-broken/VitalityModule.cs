using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalityModule : GameInteractable
{
    private void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Vector3.one;
    }

    public override bool TryInteract()
    {
        Destroy(gameObject);
        PlayerOxygen.Instance.RefreshOxygen();
        return false;
    }
}
