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
        yield return new WaitForSeconds(7);
        ScenarioManager.Instance.RunNextScenario();
        CutsceneManager.Instance.RunCutscene(_director, () =>
        {
            ShowHidden(false);
            StartCoroutine(PlayerImpact());
        });
        settings.OverrideCameraRotation = true;
        yield return base.RunScenario();
    }

    IEnumerator PlayerImpact()
    {
        CutsceneManager.Instance.Fade(1, null, duration: 0.01f);
        yield return new WaitForSeconds(8);
        CutsceneManager.Instance.Fade(0, null, duration: 7);
    }
}
