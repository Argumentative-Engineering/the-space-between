using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LenaSpinningScenario : Scenario
{
    float _spinningPercent = 100;

    bool _startSpinning = false;

    GameObject _player;

    float _currFuel;
    PlayerThruster _thruster;

    [SerializeField] DialogueData _ohFuckDialogue;
    private void Start()
    {
        _player = GameManager.Instance.Player;
        EventManager.Instance.RegisterListener(EventDefinitions.PlayerThrust, OnPlayerThrust);
        EventManager.Instance.RegisterListener(EventDefinitions.DoSpinning, OnSpinningStart);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener(EventDefinitions.PlayerThrust, OnPlayerThrust);
        EventManager.Instance.UnregisterListener(EventDefinitions.DoSpinning, OnSpinningStart);
    }

    public override IEnumerator RunScenario()
    {
        MusicManager.Instance.PlayMusic();
        yield return base.RunScenario();
    }

    private void OnPlayerThrust(object[] obj)
    {
        _spinningPercent -= Time.deltaTime * 5;
    }

    private void OnSpinningStart(object[] obj)
    {
        _startSpinning = true;
        StartCoroutine(SpinSequence());
    }

    IEnumerator SpinSequence()
    {
        yield return new WaitForSeconds(15);
        CutsceneManager.Instance.Fade(0, null, duration: 7, startBlack: true);
        PlayerSettings.Instance.CanUseThrusters = true;
        if (PlayerInventory.Instance.EquippedItem is not PlayerThruster)
            PlayerInventory.Instance.EquipItem((int)InventoryItems.Thruster, forceEquip: true);

        _thruster = PlayerInventory.Instance.EquippedItem.GetComponent<PlayerThruster>();
        _currFuel = _thruster.ThrusterFuel;

        PlayerSettings.Instance.OverrideCameraRotation = true;
        ShowHidden(false);

    }

    private void Update()
    {
        if (!_startSpinning) return;
        if (_thruster != null)
            _thruster.ThrusterFuel = Mathf.Lerp(0, _currFuel, (_spinningPercent - 10) / 100);

        if (_spinningPercent <= 10)
        {
            _startSpinning = false;
            ScenarioManager.Instance.RunNextScenario();
            return;
        }
        _player.transform.Rotate(Vector3.one, _spinningPercent * 1 * Time.deltaTime);
    }

    public void OhFuck()
    {
        NarrativeManager.Instance.PlayDialogue(_ohFuckDialogue);
    }
}
