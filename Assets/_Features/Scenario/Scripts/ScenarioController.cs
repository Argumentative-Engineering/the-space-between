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

    private void Start()
    {
        GameManager.Instance.MovePlayer(_playerStart);
    }
}
