
using FMODUnity;
using UnityEngine;

public class DialogueTrigger : TriggerInteractable
{
    [SerializeField] DialogueData _dialogueEvent;

    public override bool TryInteract()
    {
        NarrativeManager.Instance.PlayDialogue(_dialogueEvent);
        return base.TryInteract();
    }
}
