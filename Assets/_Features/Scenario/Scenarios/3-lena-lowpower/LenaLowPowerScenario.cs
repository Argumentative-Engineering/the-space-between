using System;
using System.Collections;
using FMODUnity;
using UnityEngine;

public class LenaLowPowerScenario : Scenario
{
    [SerializeField] StudioEventEmitter _lowPowerAlarmEmitter;

    private void Start()
    {
        EventManager.Instance.RegisterListener(EventDefinitions.WearingSpacesuit, OnWearingSpacesuit);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener(EventDefinitions.WearingSpacesuit, OnWearingSpacesuit);
    }

    public override IEnumerator RunScenario()
    {
        yield return new WaitForSeconds(3);
        _lowPowerAlarmEmitter.Play();

        yield return base.RunScenario();
    }

    private void OnWearingSpacesuit(object[] obj)
    {
        ScenarioKeys.Add("wearing-spacesuit", true);
    }

    public void RemovedItems()
    {
        ScenarioKeys.Add("removed-items", true);
    }
}