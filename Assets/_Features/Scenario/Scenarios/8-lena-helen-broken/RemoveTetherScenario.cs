using DG.Tweening;
using UnityEngine;

public class RemoveTetherScenario : Scenario
{
    [SerializeField] Transform _puzzleTetherPoint;
    public override void ExitScenario()
    {
        ShowHidden(false);
    }

    private void Start()
    {
        EventManager.Instance.RegisterListener("on-tether", OnTether);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("on-tether", OnTether);
    }

    private void OnTether(object[] obj)
    {
        var tether = (Transform)obj[0];
        if (tether != _puzzleTetherPoint) return;

        ScenarioManager.Instance.RunNextScenario(movePlayer: true);
    }

    public void NearPuzzleArea()
    {
        // TODO: write dialogue
        PlayerSettings.FreezePlayer(true);
        print("Dialogue: an EVA kit, as long as i'm connected to it i can move around");
        Camera.main.transform.DODynamicLookAt(_puzzleTetherPoint.position, 2).OnComplete(() =>
        {
            DOVirtual.DelayedCall(3f, () =>
              {
                  PlayerSettings.FreezePlayer(false);
              });
        });
    }
}
