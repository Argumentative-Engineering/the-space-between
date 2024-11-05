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
    GameObject _player;

    private void Start()
    {
        _player = GameManager.Instance.Player;
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
        PlayerSettings.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        PlayerInventory.Instance.DequipAll();
        NarrativeManager.Instance.PlayDialogue(_stopSpinningDialogue);
        yield return new WaitForSeconds(3);
        _player.transform.DORotate(Vector3.up * 180, 3);
        Camera.main.transform.DOLocalRotate(Vector3.zero, 3).OnComplete(() =>
        {
            _player.GetComponent<PlayerLocalInput>().SnapToRotation(Camera.main.transform.localRotation);
            _player.GetComponent<PlayerSettings>().OverrideCameraRotation = false;
        });

        yield return base.RunScenario();
    }
}
