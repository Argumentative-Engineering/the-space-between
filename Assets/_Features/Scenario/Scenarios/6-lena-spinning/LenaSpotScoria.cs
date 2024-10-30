using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenaSpotScoria : Scenario
{
    [SerializeField]
    DialogueData _stopSpinningDialogue;

    private void Start()
    {
        EventManager.Instance.RegisterListener("check-comms", CheckComms);
        EventManager.Instance.RegisterListener("look-at-scoria", LookAtScoria);
    }

    private void CheckComms(object[] obj)
    {

    }

    private void LookAtScoria(object[] obj)
    {
    }

    public override IEnumerator RunScenario()
    {
        NarrativeManager.Instance.PlayDialogue(_stopSpinningDialogue);
        return base.RunScenario();
    }
}
