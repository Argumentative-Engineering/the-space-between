using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HelenHitScenario : Scenario
{
    [SerializeField] PlayableDirector _director;

    public override IEnumerator RunScenario()
    {
        var settings = GameManager.Instance.Player.GetComponent<PlayerSettings>();
        settings.IsFrozen = true;
        PlayerInventory.Instance.DequipAll();
        yield return new WaitForSeconds(7);
        ScenarioManager.Instance.RunNextScenario(movePlayer: true);
        CutsceneManager.Instance.RunCutscene(_director, () =>
        {
            ShowHidden(false);
            CutsceneManager.Instance.Fade(1, null, duration: 0.01f);
            EventManager.Instance.BroadcastEvent(EventDefinitions.DoSpinning);
        }, unfreezePlayerOnCutsceneEnd: false);
        settings.OverrideCameraRotation = true;
        yield return base.RunScenario();
    }
}
