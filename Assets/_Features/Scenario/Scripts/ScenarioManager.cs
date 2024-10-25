using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    [ReorderableList]
    [Scene]
    public List<string> ScenarioList;

    [field: SerializeField, ReadOnly] public ScenarioController CurrentScenario { get; set; }

    public static ScenarioManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
}
