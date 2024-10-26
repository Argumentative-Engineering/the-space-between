using System.Collections;
using UnityEngine;

public class LenaMackUndockScenario : ScenarioController
{
    public override IEnumerator RunScenario()
    {
        EventManager.Instance.BroadcastEvent("start-beeping", true);
        yield return null;
    }
}