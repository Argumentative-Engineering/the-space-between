using System.Collections;
using UnityEngine;

public class GetVitalityModuleScenario : Scenario
{
    [SerializeField] Transform _vitalityModule;

    public override IEnumerator RunScenario()
    {
        PlayerOxygen.Instance.CurrentOxygen = 8;
        PlayerOxygen.Instance.StartDecreasingOxygen = true;
        return base.RunScenario();
    }
}
