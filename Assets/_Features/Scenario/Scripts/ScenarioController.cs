using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ScenarioKey
{
    public string Key;
}

public class ScenarioController : MonoBehaviour
{
    [SerializeField] Transform _playerStart;
    public Dictionary<string, dynamic> ScenarioKeys = new();
    public ScenarioController NextScenario;

    public virtual void RunScenario()
    {
        if (_playerStart != null)
            GameManager.Instance.MovePlayer(_playerStart);
    }

    public void PlayNextScenario()
    {
        NextScenario.RunScenario();
    }

    public void RegisterScenario(ScenarioController scenario)
    {
        ScenarioManager.Instance.Scenarios.Enqueue(scenario);
        if (scenario.NextScenario != null)
            ScenarioManager.Instance.Scenarios.Enqueue(scenario.NextScenario);
    }
}
