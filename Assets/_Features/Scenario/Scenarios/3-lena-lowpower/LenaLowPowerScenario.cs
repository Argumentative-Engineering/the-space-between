using System.Collections;
using FMODUnity;
using UnityEngine;

public class LenaLowPowerScenario : ScenarioController
{
    [Header("Scenario Specific")]
    [SerializeField] StudioEventEmitter _lowPowerAlarmEmitter;

    public override IEnumerator RunScenario()
    {
        yield return new WaitForSeconds(3);
        _lowPowerAlarmEmitter.Play();
    }
}