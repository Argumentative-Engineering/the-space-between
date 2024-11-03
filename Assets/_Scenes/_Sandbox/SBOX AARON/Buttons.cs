using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buttons : GameInteractable
{
    public int keypadNum = 1;

    private void Start()
    {
        Tooltip = keypadNum.ToString();
    }

    public override bool TryInteract()
    {
        OnInteract?.Invoke();
        return false;
    }
}
