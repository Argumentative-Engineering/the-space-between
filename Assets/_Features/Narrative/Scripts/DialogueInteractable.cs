using UnityEngine;

public class DialogueInteractable : GameInteractable
{
    [SerializeField] DialogueData _dialogueEvent;

    public override void Interact()
    {
        base.Interact();
        NarrativeManager.Instance.PlayDialogue(_dialogueEvent);
    }
}