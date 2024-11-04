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
        _evt.RegisterListener(EventDefinitions.DockButtonPressed, OnEnableDockButton);
        Tooltip = "";
    }

    private void OnDisable()
    {
        _evt.UnregisterListener(EventDefinitions.DockButtonPressed, OnEnableDockButton);
    }

    private void OnEnableDockButton(object[] obj)
    {
        GameManager.Instance.Player.GetComponent<PlayerInteraction>().IsInteracting = false;
        _dockButton.material.EnableKeyword("_EMISSION");
        _dockButton.material.DOColor(Color.white * Mathf.Pow(2, 3), "_EmissionColor", 0.3f);
        _canDock = true;
        Tooltip = "Undock Obsidian";
    }

    public override bool TryInteract()
    {
        if (!_canDock) return false;
        _dockButton.material.DOColor(Color.white, "_EmissionColor", 2f);
        ScenarioManager.Instance.RunNextScenario();
        return base.TryInteract();
    }
}