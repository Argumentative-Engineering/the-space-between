using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public PlayerMovementSettingsData LastPlayerMovementSettings;
    [SerializeField] Transform _lastCheckpointTransform;
    public Scenario LastActiveScenario;
    public Queue<Scenario> LastScenarioQueue = new();

    public static CheckpointManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void Save()
    {
        _lastCheckpointTransform.SetPositionAndRotation(GameManager.Instance.Player.transform.position, Camera.main.transform.rotation);
        LastPlayerMovementSettings = PlayerSettings.Instance.PlayerMovementSettings;
        LastActiveScenario = ScenarioManager.Instance.CurrentScenario;
        LastScenarioQueue = ScenarioManager.Instance.Scenarios;
    }

    public void Load()
    {
        GameManager.Instance.MovePlayer(_lastCheckpointTransform);
        PlayerSettings.Instance.UpdateSettings(LastPlayerMovementSettings);
        ScenarioManager.Instance.Scenarios.Clear();
        ScenarioManager.Instance.Scenarios = LastScenarioQueue;
        ScenarioManager.Instance.CurrentScenario = LastActiveScenario;
    }
}