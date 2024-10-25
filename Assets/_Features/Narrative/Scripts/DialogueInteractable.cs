using FMODUnity;
using UnityEngine;

public class DialogueInteractable : GameInteractable
{
    [SerializeField] EventReference _dialogueEvent;

    public override void Interact()
    {
        base.Interact();
        NarrativeManager.Instance.PlaySequence(_dialogueEvent);
    }
}