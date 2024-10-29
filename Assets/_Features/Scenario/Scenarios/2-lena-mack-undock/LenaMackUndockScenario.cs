using System.Collections;
using UnityEngine;

public class LenaMackUndockScenario : Scenario
{
    public override IEnumerator RunScenario()
    {
        EventManager.Instance.BroadcastEvent("start-beeping", true);
        yield return null;
    }
}