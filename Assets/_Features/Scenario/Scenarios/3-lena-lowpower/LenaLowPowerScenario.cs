using System.Collections;
using FMODUnity;
using UnityEngine;

public class LenaLowPowerScenario : ScenarioController
{
    [SerializeField] StudioEventEmitter _lowPowerAlarmEmitter;

    public override IEnumerator RunScenario()
    {
        yield return new WaitForSeconds(3);
        _lowPowerAlarmEmitter.Play();
    }
}