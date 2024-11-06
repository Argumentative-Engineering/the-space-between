using System;
using System.Collections;
using UnityEngine;

public class LenaSwingScenario : Scenario
{
    private void Start()
    {
        EventManager.Instance.RegisterListener("finished-fb", OnFinishFb);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("finished-fb", OnFinishFb);
    }

    private void OnFinishFb(object[] obj) => NextScene();

    public override IEnumerator RunScenario()
    {
        CutsceneManager.Instance.Fade(0, null, startBlack: true);
        PlayerSettings.Instance.CanUseTether = false;
        PlayerSettings.Instance.CanUseThrusters = false;
        PlayerOxygen.Instance.StartDecreasingOxygen = false;
        PlayerInventory.Instance.DequipAll();
        return base.RunScenario();
    }


    public void NextScene()
    {
        PlayerSettings.Instance.CanUseTether = true;
        PlayerSettings.Instance.CanUseThrusters = true;
        FlashbackManager.Instance.ExitFlashback();
    }

}
