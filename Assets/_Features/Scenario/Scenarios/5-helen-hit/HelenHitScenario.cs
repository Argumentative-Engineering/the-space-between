using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelenHitScenario : Scenario
{
    public override IEnumerator RunScenario()
    {
        GameManager.Instance.Player.GetComponent<PlayerSettings>().IsFrozen = true;
        return base.RunScenario();
    }
}
