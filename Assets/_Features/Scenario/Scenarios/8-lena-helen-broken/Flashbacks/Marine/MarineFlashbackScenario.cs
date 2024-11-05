using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineFlashbackScenario : Scenario
{
    public override IEnumerator RunScenario()
    {
        PlayerOxygen.Instance.StartDecreasingOxygen = false;
        CutsceneManager.Instance.Fade(0, null, startBlack: true);
        return base.RunScenario();
    }
}
