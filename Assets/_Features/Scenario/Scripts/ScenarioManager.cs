using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public Queue<ScenarioController> Scenarios = new();

    [field: SerializeField, ReadOnly] public ScenarioController CurrentScenario { get; set; }

    public static ScenarioManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void RunNextScenario()
    {
        if (Scenarios.TryDequeue(out ScenarioController scenario))
        {
            CurrentScenario = scenario;
            CurrentScenario.RunScenario();
        }
    }
    public void RunScenario(ScenarioController scenario)
    {
        CurrentScenario = scenario;
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            RunNextScenario();
        }
    }
#endif

}
