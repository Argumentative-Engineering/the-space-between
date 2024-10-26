using UnityEngine;

public class LenaMackUndockScenario : ScenarioController
{
    public override void RunScenario()
    {
        EventManager.Instance.BroadcastEvent("start-beeping", true);
    }
}