using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using UnityEngine;

public class MoveAnchor : GameInteractable
{
    [SerializeField] Transform _orientation;

    private void OnValidate()
    {
        Tooltip = "Start";
        _orientation = transform.GetChild(0);
    }

    public override bool TryInteract()
    {
        var pos = _orientation.transform.position - Vector3.up;
        _orientation.position = pos;
        GameManager.Instance.MovePlayer(_orientation, rotate: false);
        return false;
    }
}
