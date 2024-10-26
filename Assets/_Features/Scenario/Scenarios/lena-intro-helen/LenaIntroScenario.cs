public class LenaIntroScenario : ScenarioController
{
    private void Awake()
    {
        ScenarioKeys.Add("item-checked-count", 0);
    }

    private void Start()
    {
        RegisterScenario(this);
        ScenarioManager.Instance.RunNextScenario();
    }

    public void CheckedItem()
    {
        var count = ScenarioKeys["item-checked-count"];
        count++;
        ScenarioKeys["item-checked-count"] = count;

        if (count == 3)
        {
            Invoke(nameof(StartBeeping), 5);
        }
    }

    void StartBeeping()
    {
        ScenarioManager.Instance.RunScenario(NextScenario);
    }
}