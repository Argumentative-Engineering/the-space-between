using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineFlashback : GameInteractable
{
    [SerializeField] MeshCollider _canadarm;
    FixScoriaScenario _scen;
    private void Start()
    {
        _canadarm.enabled = false;
    }

    public override bool TryInteract()
    {
        _scen = ScenarioManager.Instance.GetScenario<FixScoriaScenario>();
        _scen.MarineFlashbackDone = true;
        _canadarm.enabled = true;
        FlashbackManager.Instance.RunFlashback(SceneDefinitions.MarineFlashback);
        return base.TryInteract();
    }
}
