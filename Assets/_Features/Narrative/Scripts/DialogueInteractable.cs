using UnityEngine;

public class DialogueInteractable : GameInteractable
{
    [SerializeField] DialogueData _dialogueEvent;

    public override bool TryInteract()
    {
        if (NarrativeManager.Instance.IsRunning) return false;
        NarrativeManager.Instance.PlayDialogue(_dialogueEvent);

        return base.TryInteract();
    }
}