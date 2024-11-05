using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineFlashbackScenario : Scenario
{
    private void Start()
    {
        EventManager.Instance.RegisterListener("finished-fb", OnFinishedFlashback);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("finished-fb", OnFinishedFlashback);
    }

    private void OnFinishedFlashback(object[] obj)
    {
        PlayerSettings.Instance.CanUseTether = true;
        PlayerSettings.Instance.CanUseThrusters = true;
        FlashbackManager.Instance.ExitFlashback();
    }

    public override IEnumerator RunScenario()
    {
        PlayerOxygen.Instance.StartDecreasingOxygen = false;
        CutsceneManager.Instance.Fade(0, null, startBlack: true);
        return base.RunScenario();
    }
}
