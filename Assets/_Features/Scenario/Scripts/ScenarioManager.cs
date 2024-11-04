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
        RunNextScenario(movePlayer: true);
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
        UnloadScenarios();
        var scenarios = FindObjectsOfType<Scenario>().ToList().OrderBy(s => s.ScenarioName);
        foreach (var scenario in scenarios)
            Scenarios.Enqueue(scenario);
    }

    public void RunNextScenario(bool movePlayer = false)
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
            CurrentScenario.MovePlayerOnRun = movePlayer;
            _currScenarioCoroutine = StartCoroutine(CurrentScenario.RunScenario());
        }
    }

    public void RunScenario(Scenario scenario)
    {
        CurrentScenario = scenario;
    }

    public T GetScenario<T>() where T : Scenario
    {
        foreach (var scenario in Scenarios)
        {
            if (scenario is T specificScenario)
                return specificScenario;
        }

        if (CurrentScenario is T currentSpecific)
            return currentSpecific;

        return null;
    }
}
