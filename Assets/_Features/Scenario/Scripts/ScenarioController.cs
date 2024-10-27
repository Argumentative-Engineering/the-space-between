using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenarioKey
{
    public string Key;
}

public class ScenarioController : MonoBehaviour
{
    // for tracking purposes
    public string ScenarioName;
    [SerializeField] Transform _playerStart;
    public Dictionary<string, dynamic> ScenarioKeys = new();
    [SerializeField] PlayerMovementSettingsData _playerSettingsData;
    public virtual IEnumerator RunScenario()
    {
        if (_playerStart != null)
            GameManager.Instance.MovePlayer(_playerStart);

        if (_playerSettingsData != null)
            GameManager.Instance.Player.GetComponent<PlayerSettings>().UpdateSettings(_playerSettingsData);

        yield return null;
    }
}
