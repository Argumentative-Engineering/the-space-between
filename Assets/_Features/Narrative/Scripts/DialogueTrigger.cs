
using FMODUnity;
using UnityEngine;

public class DialogueTrigger : TriggerInteractable
{
    [SerializeField] DialogueData _dialogueEvent;

    protected override void Trigger()
    {
        NarrativeManager.Instance.PlayDialogue(_dialogueEvent);
    }
}
