using System.Collections.Generic;
using UnityEngine;

public class ScenarioController : MonoBehaviour
{
    [SerializeField] Transform _playerStart;
    public Dictionary<string, dynamic> ScenarioKeys { get; private set; } = new();

    private void Start()
    {
        GameManager.Instance.MovePlayer(_playerStart);
    }
}
