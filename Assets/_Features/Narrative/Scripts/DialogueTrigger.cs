
using FMODUnity;
using UnityEngine;

public class DialogueTrigger : TriggerInteractable
{
    [SerializeField] DialogueData _dialogueEvent;

    protected override void Trigger()
    {
        if (NarrativeManager.Instance.IsRunning) return;
        NarrativeManager.Instance.PlayDialogue(_dialogueEvent);
    }
}
