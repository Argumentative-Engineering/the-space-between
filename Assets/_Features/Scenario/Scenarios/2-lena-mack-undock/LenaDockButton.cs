using DG.Tweening;
using UnityEngine;

public class LenaDockButton : DialogueInteractable
{
    EventManager _evt;
    [SerializeField] Renderer _dockButton;
    bool _canDock = false;

    private void Start()
    {
        _evt = EventManager.Instance;
        _evt.RegisterListener("dock-button", DockButton);
    }

    private void OnDisable()
    {
        _evt.RegisterListener("dock-button", DockButton);
    }

    private void DockButton(object[] obj)
    {
        _dockButton.material.EnableKeyword("_EMISSION");
        _dockButton.material.DOColor(Color.white * Mathf.Pow(2, 3), "_EmissionColor", 0.3f);
        _canDock = true;
    }

    public override bool TryInteract()
    {
        if (!_canDock) return false;
        if (!base.TryInteract()) return false;
        _dockButton.material.DOColor(Color.white, "_EmissionColor", 0.3f).OnComplete(() => NextSeq());
        return true;
    }

    void NextSeq()
    {
        ScenarioManager.Instance.RunNextScenario();
    }
}