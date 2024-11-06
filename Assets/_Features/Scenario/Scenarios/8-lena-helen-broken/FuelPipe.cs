using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPipe : GameInteractable
{
    [SerializeField] bool _isProperPipe;
    [SerializeField] DialogueData _dialogue;

    public override bool TryInteract()
    {
        EventManager.Instance.BroadcastEvent(EventDefinitions.CheckPipe, _isProperPipe, gameObject);
        NarrativeManager.Instance.PlayDialogue(_dialogue);
        return base.TryInteract();
    }
}
