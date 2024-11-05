using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Playables;

public class HelenHitScenario : Scenario
{
    [SerializeField] PlayableDirector _director;
    [SerializeField] EventReference _hitBoom;

    public override IEnumerator RunScenario()
    {
        var settings = GameManager.Instance.Player.GetComponent<PlayerSettings>();
        settings.IsFrozen = true;
        PlayerInventory.Instance.DequipAll();
        yield return new WaitForSeconds(2);
        CutsceneManager.Instance.RunCutscene(_director, () =>
        {
            ShowHidden(false);
            RuntimeManager.PlayOneShot(_hitBoom);
            ScenarioManager.Instance.RunNextScenario(movePlayer: true);
            CutsceneManager.Instance.Fade(1, null, duration: 0.01f, startBlack: true);
            EventManager.Instance.BroadcastEvent(EventDefinitions.DoSpinning);
        }, unfreezePlayerOnCutsceneEnd: false);
        settings.OverrideCameraRotation = true;
        yield return base.RunScenario();
    }
}
