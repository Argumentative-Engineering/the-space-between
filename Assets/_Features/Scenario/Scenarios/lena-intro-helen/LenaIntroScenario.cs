
using UnityEngine;

public class LenaIntroScenario : ScenarioController
{
    private void Awake()
    {
        ScenarioKeys.Add("item-checked-count", 0);
    }

    public void CheckedItem()
    {
        var count = ScenarioKeys["item-checked-count"];
        count++;
        ScenarioKeys["item-checked-count"] = count;
        if (count == 3)
        {
            Invoke(nameof(StartBeeping), 3);
        }
    }

    void StartBeeping()
    {
        EventManager.Instance.BroadcastEvent("start-beeping", true);
    }
}