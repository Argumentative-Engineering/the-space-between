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

    public virtual IEnumerator RunScenario()
    {
        if (_playerStart != null)
            GameManager.Instance.MovePlayer(_playerStart);

        yield return null;
    }
}
