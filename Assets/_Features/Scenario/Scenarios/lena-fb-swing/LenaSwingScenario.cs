using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenaSwingScenario : ScenarioController
{
    private void Start()
    {
        ScenarioManager.Instance.RunNextScenario();
    }
}
