using System;
using System.Collections;
using UnityEngine;

public class LenaSwingScenario : Scenario
{
    bool _complete;
    int _pushCount = 0;
    private void Start()
    {
        EventManager.Instance.RegisterListener("push-swing", OnPushSwing);
    }

    public override IEnumerator RunScenario()
    {
        PlayerSettings.Instance.CanUseTether = false;
        PlayerSettings.Instance.CanUseThrusters = false;
        PlayerOxygen.Instance.StartDecreasingOxygen = false;
        PlayerInventory.Instance.DequipAll();
        CutsceneManager.Instance.Fade(0, null, startBlack: true);
        return base.RunScenario();
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("push-swing", OnPushSwing);
    }

    private void OnPushSwing(object[] obj)
    {
        _pushCount = (int)obj[0];

        if (_pushCount == 5 && !NarrativeManager.Instance.IsRunning && !_complete)
        {
            _complete = true;
            Invoke(nameof(NextScene), 3);
        }
    }

    public void NextScene()
    {
        PlayerSettings.Instance.CanUseTether = true;
        PlayerSettings.Instance.CanUseThrusters = true;
        FlashbackManager.Instance.ExitFlashback();
    }

    public void KidYell()
    {
        print("kid yelling lol");
    }
}
