using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class DialogueTrigger : GameTrigger
{
    [SerializeField] EventReference _dialogueEvent;

    protected override void ProcessTrigger()
    {
        NarrativeManager.Instance.PlaySequence(_dialogueEvent);
    }
}
