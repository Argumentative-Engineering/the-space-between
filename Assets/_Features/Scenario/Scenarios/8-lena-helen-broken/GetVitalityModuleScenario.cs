using System.Collections;
using UnityEngine;

public class GetVitalityModuleScenario : Scenario
{
    [SerializeField] Transform _vitalityModule;

    bool _start = false;

    public override IEnumerator RunScenario()
    {
        CutsceneManager.Instance.Fade(1, null, duration: 0.01f, startBlack: true);
        yield return base.RunScenario();
        CutsceneManager.Instance.Fade(0, () => _start = false, duration: 3, startBlack: true);
        yield return new WaitUntil(() => _start);
        PlayerOxygen.Instance.CurrentOxygen = 8;
        PlayerOxygen.Instance.StartDecreasingOxygen = true;
    }
}
