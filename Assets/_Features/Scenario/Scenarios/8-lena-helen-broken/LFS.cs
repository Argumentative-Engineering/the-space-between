using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFS : DialogueInteractable
{
    public override bool TryInteract()
    {
        base.TryInteract();

        Destroy(gameObject);
        return false;
    }
}
