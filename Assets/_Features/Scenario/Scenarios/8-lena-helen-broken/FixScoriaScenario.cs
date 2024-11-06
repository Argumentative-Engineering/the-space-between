using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScoriaScenario : Scenario
{
    public bool MarineFlashbackDone = false;
    public bool ZekeFlashbackDone = false;

    public bool CanLookAtPowerCell { get; set; }
    public bool LookedAtPowerCell { get; set; }

    public int PowerCellConnectionCount;
    public bool AreFuelLinesFixed;

    public bool HasFuelPipe;

    [Header("Dialogue Lines")]
    [SerializeField] DialogueData _notProperPipe;
    [SerializeField] DialogueData _properPipe;
    [SerializeField] DialogueData _lookAtCell;
    [SerializeField] DialogueData _powercellConnected;

    [Header("References")]
    [SerializeField] Transform _euler;

    public bool IsScoriaWorking()
    {
        return PowerCellConnectionCount >= 2
            && AreFuelLinesFixed;
    }

    private void Start()
    {
        EventManager.Instance.RegisterListener(EventDefinitions.CheckPipe, OnCheckPipe);
        EventManager.Instance.RegisterListener("look-at-power-cell", OnLookPowercell);
        EventManager.Instance.RegisterListener("look-euler", OnLookEuler);
    }
    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener(EventDefinitions.CheckPipe, OnCheckPipe);
        EventManager.Instance.UnregisterListener("look-at-power-cell", OnLookPowercell);
        EventManager.Instance.UnregisterListener("look-euler", OnLookEuler);
    }
    private void OnLookPowercell(object[] obj)
    {
        var pos = (Vector3)obj[0];
        LookedAtPowerCell = true;
        AnimationUtils.AnimateLookAt(pos, zoom: true, duration: 1, delay: 4, onComplete: () =>
        {
            NarrativeManager.Instance.PlayDialogue(_lookAtCell);
        });
    }

    private void OnCheckPipe(object[] obj)
    {
        var isProper = (bool)obj[0];
        var pipe = (GameObject)obj[1];

        if (isProper)
        {
            HasFuelPipe = true;
            Destroy(pipe);
        }
    }

    private void OnLookEuler(object[] obj)
    {
        AnimationUtils.AnimateLookAt(_euler.position, zoom: true, duration: 1, delay: 3);
    }

    public void Complete()
    {
        GameManager.Instance.LoadLevel(SceneDefinitions.InsideScoria);
    }

    public void ConnectPowercell()
    {
        PowerCellConnectionCount++;

        if (PowerCellConnectionCount == 2)
            NarrativeManager.Instance.PlayDialogue(_powercellConnected);
    }
}
