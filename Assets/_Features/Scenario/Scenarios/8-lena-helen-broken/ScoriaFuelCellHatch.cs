using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoriaFuelCellHatch : GameInteractable
{
    public override bool TryInteract()
    {
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(transform.right + transform.up * 5, ForceMode.Impulse);
        base.TryInteract();
        return false;
    }
}
