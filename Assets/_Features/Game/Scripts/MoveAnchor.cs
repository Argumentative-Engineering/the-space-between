using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using UnityEngine;

public class MoveAnchor : GameInteractable
{
    [SerializeField] Transform _orientation;
    public bool CanPushAway = true;

    private void OnValidate()
    {
        Tooltip = "Grab";
        _orientation = transform.GetChild(0);
    }

    public override bool TryInteract()
    {
        if (Vector3.Distance(GameManager.Instance.Player.transform.position, transform.position) > 5)
        {
            // TODO(dialogue): its too far
            print("Dialogue: It's too far");
            return false;
        }
        GameManager.Instance.MovePlayer(_orientation, offset: -Vector3.up, snap: false, rotate: false);
        PlayerSettings.Instance.IsAnchored = CanPushAway;
        PlayerSettings.Instance.GetComponent<PlayerInteraction>().SetTooltip("Push yourself");
        return false;
    }
}
