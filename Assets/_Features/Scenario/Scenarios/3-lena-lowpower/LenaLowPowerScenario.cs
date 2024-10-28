using System;
using System.Collections;
using FMODUnity;
using UnityEngine;

public class LenaLowPowerScenario : ScenarioController
{
    [SerializeField] StudioEventEmitter _lowPowerAlarmEmitter;

    private void Start()
    {
        EventManager.Instance.RegisterListener("wearing-spacesuit", WearingSpacesuit);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("wearing-spacesuit", WearingSpacesuit);
    }

    public override IEnumerator RunScenario()
    {
        yield return new WaitForSeconds(3);
        _lowPowerAlarmEmitter.Play();

        yield return base.RunScenario();
    }

    private void WearingSpacesuit(object[] obj)
    {
        ScenarioKeys.Add("wearing-spacesuit", true);
    }

    public void RemovedItems()
    {
        ScenarioKeys.Add("removed-items", true);
    }
}