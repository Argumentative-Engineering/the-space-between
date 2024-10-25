
using FMODUnity;
using UnityEngine;

public class DialogueTrigger : TriggerInteractable
{
    [SerializeField] EventReference _dialogueEvent;

    protected override void Trigger()
    {
        NarrativeManager.Instance.PlaySequence(_dialogueEvent);
    }
}
