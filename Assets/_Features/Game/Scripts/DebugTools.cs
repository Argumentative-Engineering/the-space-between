using UnityEngine;

public class DebugTools : MonoBehaviour
{
#if UNITY_EDITOR
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