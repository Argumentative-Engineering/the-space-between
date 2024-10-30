using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public Queue<Scenario> Scenarios = new();

    [field: SerializeField, ReadOnly] public Scenario CurrentScenario { get; set; }
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
        CurrentScenario = null;
        Scenarios.Clear();
    }

    public void LoadScenarios()
    {
        var scenarios = FindObjectsOfType<Scenario>().ToList().OrderBy(s => s.ScenarioName);
        foreach (var scenario in scenarios)
            Scenarios.Enqueue(scenario);
    }

    public void RunNextScenario()
    {
        if (Scenarios.TryDequeue(out Scenario scenario))
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
            CurrentScenario.enabled = true;
            _currScenarioCoroutine = StartCoroutine(CurrentScenario.RunScenario());
        }
    }

    public void RunScenario(Scenario scenario)
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
        if (Scenarios.Count == 0) return;
        var scens = "Next: ";
        foreach (var scenario in Scenarios)
        {
            scens += $"{scenario.name} ";
        }
        GUI.Label(new Rect(20, 48, 1000, 1000), $"{scens}");
    }
#endif

}
