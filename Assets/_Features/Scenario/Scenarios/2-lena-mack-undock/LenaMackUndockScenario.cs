using System.Collections;
using UnityEngine;

public class LenaMackUndockScenario : Scenario
{
    public override IEnumerator RunScenario()
    {
        EventManager.Instance.BroadcastEvent(EventDefinitions.StartBeeping, true);
        yield return null;
    }
}