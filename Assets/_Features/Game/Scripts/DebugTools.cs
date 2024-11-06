using UnityEngine;

public class DebugTools : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] PlayerMovementSettingsData _freeCamMovement;

    PlayerMovementSettingsData _prevSettings;
    bool _isFreeCam;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerOxygen.Instance.CurrentOxygen = 0;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ScenarioManager.Instance.RunNextScenario(movePlayer: true);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerOxygen.Instance.RefreshOxygen();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            GameObject.FindObjectOfType<CanadarmTip>().DetachFuelCell();
        }

        if (Input.GetKeyDown(KeyCode.Semicolon)) ToggleFreeCamMovement();
    }

    void ToggleFreeCamMovement()
    {
        if (_isFreeCam)
        {
            PlayerSettings.Instance.UpdateSettings(_prevSettings);
            PlayerSettings.Instance.CanUseTether = true;
            _isFreeCam = false;
            return;
        }

        _prevSettings = PlayerSettings.Instance.PlayerMovementSettings;
        PlayerSettings.Instance.UpdateSettings(_freeCamMovement);
        _isFreeCam = true;
    }

    private void OnGUI()
    {
        DebugScenarios();
    }

    void DebugScenarios()
    {
        var curr = ScenarioManager.Instance.CurrentScenario;
        if (curr == null) return;
        var scens = ScenarioManager.Instance.Scenarios;

        GUI.Label(new Rect(19, 30, 1000, 1000), $"> {curr.name}");
        if (scens.Count == -1) return;
        var scenList = "";
        foreach (var scenario in scens)
        {
            scenList += $"\t\n{scenario.name} ";
        }
        GUI.Label(new Rect(19, 48, 1000, 1000), $"{scenList}");

    }
#endif
}