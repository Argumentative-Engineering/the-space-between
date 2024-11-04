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
        EventManager.Instance.RegisterListener("check-comms", OnCheckComms);
        EventManager.Instance.RegisterListener(EventDefinitions.LookAtScoria, OnLookAtScoria);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("check-comms", OnCheckComms);
        EventManager.Instance.UnregisterListener(EventDefinitions.LookAtScoria, OnLookAtScoria);
    }

    private void OnCheckComms(object[] obj)
    {
        print("checking comms");
    }

    private void OnLookAtScoria(object[] obj)
    {
        var prevRot = Camera.main.transform.rotation;
        GameManager.Instance.Player.GetComponent<PlayerSettings>().IsFrozen = true;
        var seq = DOTween.Sequence();
        seq.Append(Camera.main.transform.DOLookAt(_scoria.position, 2)).AppendInterval(5).AppendCallback(() =>
        {
            GameManager.Instance.Player.GetComponent<PlayerSettings>().IsFrozen = false;
            CutsceneManager.Instance.Fade(1, null, duration: 0.001f);
            GameManager.Instance.LoadLevel(SceneDefinitions.HelenBroken);
        });
        seq.Play();
    }

    public override IEnumerator RunScenario()
    {
        NarrativeManager.Instance.PlayDialogue(_stopSpinningDialogue);
        return base.RunScenario();
    }
}
