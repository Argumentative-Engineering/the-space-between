using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class ScenarioKey
{
    public string Key;
}

public class Scenario : MonoBehaviour
{
    // for tracking purposes
    public string ScenarioName;
    [SerializeField] Transform _playerStart;
    public Dictionary<string, dynamic> ScenarioKeys = new();
    [field: SerializeField] public bool MovePlayerOnRun { get; set; }
    [SerializeField] bool _snapPlayerOnMove = false;
    [SerializeField] bool _flashlightEnabled = true;
    [SerializeField] PlayerMovementSettingsData _playerSettingsData;


    [Header("Scenario Specific")]
    [SerializeField] GameObject[] _hiddenTillRun;

    private void Awake()
    {
        ShowHidden(false);
    }

    public virtual IEnumerator RunScenario()
    {
        if (_playerStart != null && MovePlayerOnRun)
            GameManager.Instance.MovePlayer(_playerStart, snap: _snapPlayerOnMove);

        if (_playerSettingsData != null)
            GameManager.Instance.Player.GetComponent<PlayerSettings>().UpdateSettings(_playerSettingsData);

        var flashlight = GameManager.Instance.Player.GetComponentInChildren<Light>();
        if (flashlight != null)
            flashlight.enabled = _flashlightEnabled;

        ShowHidden(true);
        yield return null;
    }

    protected void ShowHidden(bool visiblity)
    {
        if (_hiddenTillRun == null) return;
        foreach (var hidden in _hiddenTillRun)
        {
            hidden.SetActive(visiblity);
        }
    }

    public virtual void ExitScenario()
    {
        var dirs = FindObjectsOfType<PlayableDirector>();

        foreach (var dir in dirs)
            dir.Stop();
    }
}
