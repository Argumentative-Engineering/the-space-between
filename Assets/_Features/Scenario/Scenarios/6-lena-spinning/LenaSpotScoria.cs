using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LenaSpotScoria : Scenario
{
    [SerializeField]
    DialogueData _stopSpinningDialogue;
    [SerializeField] Transform _scoria;

    private void Start()
    {
        EventManager.Instance.RegisterListener("check-comms", CheckComms);
        EventManager.Instance.RegisterListener("look-at-scoria", LookAtScoria);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("check-comms", CheckComms);
        EventManager.Instance.UnregisterListener("look-at-scoria", LookAtScoria);
    }

    private void CheckComms(object[] obj)
    {
        print("checking comms");
    }

    private void LookAtScoria(object[] obj)
    {
        var prevRot = Camera.main.transform.rotation;
        GameManager.Instance.Player.GetComponent<PlayerSettings>().IsFrozen = true;
        var seq = DOTween.Sequence();
        seq.Append(Camera.main.transform.DOLookAt(_scoria.position, 2))
            .Append(Camera.main.transform.DORotate(prevRot.eulerAngles, 2).SetDelay(5))
            .OnComplete(() =>
            {
                GameManager.Instance.Player.GetComponent<PlayerSettings>().IsFrozen = false;
                CutsceneManager.Instance.Fade(1, null, duration: 0.001f);
                GameManager.Instance.LoadLevel(SceneDefinitions.SwingFlashback);
            });
    }

    public override IEnumerator RunScenario()
    {
        NarrativeManager.Instance.PlayDialogue(_stopSpinningDialogue);
        return base.RunScenario();
    }
}
