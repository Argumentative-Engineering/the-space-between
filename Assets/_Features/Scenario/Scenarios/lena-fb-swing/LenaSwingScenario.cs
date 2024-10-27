
using System;
using UnityEngine;

public class LenaSwingScenario : ScenarioController
{
    int _pushCount = 0;
    private void Start()
    {
        EventManager.Instance.RegisterListener("push-swing", OnPushSwing);
    }

    private void OnPushSwing(object[] obj)
    {
        _pushCount = (int)obj[0];
    }

    public void KidYell()
    {
        print("kid yelling lol");
    }
}
