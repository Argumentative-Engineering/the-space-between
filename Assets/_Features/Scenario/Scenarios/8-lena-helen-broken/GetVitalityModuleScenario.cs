using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GetVitalityModuleScenario : Scenario
{
    [SerializeField] Transform _vitalityModule, _prop;
    [SerializeField] Transform _tether, _tetherPoint;
    [SerializeField] DialogueData _wakeUpDialogue;
    [SerializeField] DialogueData _oxygenRefreshDialogue;

    bool _start = false;

    bool _didVitalityModule;

    private void Start()
    {
        EventManager.Instance.RegisterListener(EventDefinitions.LookAtVitalityModule, OnLookAtVM);
        EventManager.Instance.RegisterListener(EventDefinitions.AimVM, OnAimVM);
        EventManager.Instance.RegisterListener(EventDefinitions.PropUp, OnPropUp);
        EventManager.Instance.RegisterListener(EventDefinitions.AimTether, OnAimTether);
        EventManager.Instance.RegisterListener(EventDefinitions.RefreshOxygen, OnOxygenRefresh);
        EventManager.Instance.RegisterListener("look-tether-point", OnLookTetherPoint);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener(EventDefinitions.LookAtVitalityModule, OnLookAtVM);
        EventManager.Instance.UnregisterListener(EventDefinitions.AimVM, OnAimVM);
        EventManager.Instance.UnregisterListener(EventDefinitions.PropUp, OnPropUp);
        EventManager.Instance.UnregisterListener(EventDefinitions.AimTether, OnAimTether);
        EventManager.Instance.UnregisterListener(EventDefinitions.RefreshOxygen, OnOxygenRefresh);
        EventManager.Instance.UnregisterListener("look-tether-point", OnLookTetherPoint);
    }

    private void OnLookAtVM(object[] obj)
        => AnimationUtils.AnimateLookAt(_vitalityModule.position, zoom: true);

    private void OnPropUp(object[] obj)
        => AnimationUtils.AnimateLookAt(_prop.position, duration: 1, delay: 1);

    private void OnAimVM(object[] obj)
        => AnimationUtils.AnimateLookAt(_vitalityModule.position);

    private void OnAimTether(object[] obj)
    { }

    private void OnOxygenRefresh(object[] obj)
    {
        if (_didVitalityModule) return;

        _didVitalityModule = true;
        NarrativeManager.Instance.PlayDialogue(_oxygenRefreshDialogue);
    }

    void OnLookTetherPoint(object[] obj)
        => AnimationUtils.AnimateLookAt(_tetherPoint.position, onComplete: () =>
        {
            GameHints.Instance.ShowHint("Press LMB to throw tether");
        });

    public override IEnumerator RunScenario()
    {
        yield return new WaitForSeconds(5);
        PlayerSettings.FreezePlayer(true);
        NarrativeManager.Instance.PlayDialogue(_wakeUpDialogue);
        PlayerOxygen.Instance.CurrentOxygen = 12;
        PlayerOxygen.Instance.StartDecreasingOxygen = true;
        yield return base.RunScenario();
        yield return new WaitForSeconds(5);
        CutsceneManager.Instance.Fade(0, () => _start = false, duration: 3, startBlack: true);
        yield return new WaitUntil(() => _start);
    }
}
