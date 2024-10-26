using System.Collections;
using UnityEngine;

public class LenaIntroScenario : ScenarioController
{
    private void Awake()
    {
        ScenarioKeys.Add("item-checked-count", 0);
    }

    private void Start()
    {
        ScenarioManager.Instance.RunNextScenario();
        EventManager.Instance.RegisterListener("item-checked", CheckedItem);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("item-checked", CheckedItem);
    }

    public void CheckedItem(object[] obj)
    {
        var count = ScenarioKeys["item-checked-count"];
        count++;
        ScenarioKeys["item-checked-count"] = count;

        if (count == 3)
            Invoke(nameof(StartBeeping), 5);
    }

    void StartBeeping()
    {
        ScenarioManager.Instance.RunNextScenario();
    }
}