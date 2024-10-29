using UnityEngine;

public class LenaCleanPanelScenario : Scenario
{
    int _cleanedPanels;

    public void CleanedPanel()
    {
        _cleanedPanels++;

        print(_cleanedPanels);
        if (_cleanedPanels == 3)
        {
            ScenarioManager.Instance.RunNextScenario();
        }
    }
}