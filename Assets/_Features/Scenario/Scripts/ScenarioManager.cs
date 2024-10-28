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

        LoadScenarios();
        RunNextScenario();
    }

    public void UnloadScenarios()
    {
        if (_currScenarioCoroutine != null)
        {
            StopCoroutine(_currScenarioCoroutine);
            _currScenarioCoroutine = null;
        }

        Scenarios.Clear();
    }

    public void LoadScenarios()
    {
        var scenarios = FindObjectsOfType<ScenarioController>().ToList().OrderBy(s => s.ScenarioName);
        foreach (var scenario in scenarios)
            Scenarios.Enqueue(scenario);
    }

    public void RunNextScenario()
    {
        if (Scenarios.TryDequeue(out ScenarioController scenario))
        {
            if (CurrentScenario != null)
                CurrentScenario.ExitScenario();

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
        if (Scenarios.Count == 0) return;
        GUI.Label(new Rect(20, 30, 1000, 1000), $"Current Scenario: {CurrentScenario.name}");
        var scens = "";

        foreach (var scenario in Scenarios)
        {
            scens += $"{scenario.name} ";
        }
        GUI.Label(new Rect(20, 48, 1000, 1000), $"{scens}");
    }
#endif

}
