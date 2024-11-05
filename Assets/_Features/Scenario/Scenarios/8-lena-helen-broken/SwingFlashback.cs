using UnityEngine;

public class SwingFlashback : GameInteractable
{
    [SerializeField] FixScoriaScenario _scen;

    public override bool TryInteract()
    {
        _scen.ZekeFlashbackDone = true;
        FlashbackManager.Instance.RunFlashback(SceneDefinitions.SwingFlashback);
        return base.TryInteract();
    }
}