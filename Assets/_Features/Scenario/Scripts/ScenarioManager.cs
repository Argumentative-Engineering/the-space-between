using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public Queue<ScenarioController> Scenarios = new();

    [field: SerializeField, ReadOnly] public ScenarioController CurrentScenario { get; set; }
    Coroutine _currScenarioCoroutine;

    public static ScenarioManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;

        // look for all scenarios and queue
        var scenarios = FindObjectsOfType<ScenarioController>().ToList().OrderBy(s => s.ScenarioName);
        foreach (var scenario in scenarios)
        {
            Scenarios.Enqueue(scenario);
        }


        RunNextScenario();
    }

    public void RunNextScenario()
    {
        if (Scenarios.TryDequeue(out ScenarioController scenario))
        {
            if (_currScenarioCoroutine != null)
            {
                StopCoroutine(_currScenarioCoroutine);
                _currScenarioCoroutine = null;
            }
            print("Running scenario: " + scenario.name);
            CurrentScenario = scenario;
            _currScenarioCoroutine = StartCoroutine(CurrentScenario.RunScenario());
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

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 30, 1000, 1000), $"Current Scenario: {CurrentScenario.name}");
    }
#endif

}
