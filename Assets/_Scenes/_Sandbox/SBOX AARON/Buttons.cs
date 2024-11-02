using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buttons : GameInteractable
{
    public int keypadNum = 1;
    public UnityEvent OnKeypadClicked;

    public override bool TryInteract()
    {
        OnKeypadClicked.Invoke();
        return base.TryInteract();
    }
}
