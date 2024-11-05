using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixScoriaScenario : Scenario
{
    public bool MarineFlashbackDone = false;
    public bool ZekeFlashbackDone = false;

    public int PowerCellConnectionCount;
    public bool AreFuelLinesFixed;

    public bool HasFuelPipe;

    [Header("Dialogue Lines")]
    [SerializeField] DialogueData _notProperPipe;
    [SerializeField] DialogueData _properPipe;

    public bool IsScoriaWorking()
    {
        return PowerCellConnectionCount == 2
            && AreFuelLinesFixed;
    }

    private void Start()
    {
        EventManager.Instance.RegisterListener(EventDefinitions.CheckPipe, OnCheckPipe);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener(EventDefinitions.CheckPipe, OnCheckPipe);
    }

    private void OnCheckPipe(object[] obj)
    {
        var isProper = (bool)obj[0];
        var pipe = (GameObject)obj[1];

        if (isProper)
        {
            NarrativeManager.Instance.PlayDialogue(_properPipe);
            HasFuelPipe = true;
            Destroy(pipe);
        }
        else
        {
            NarrativeManager.Instance.PlayDialogue(_notProperPipe);
        }
    }

    public void Complete()
    {
        StartCoroutine(CompleteSequence());
    }

    IEnumerator CompleteSequence()
    {
        yield return new WaitForSeconds(10);
        GameManager.Instance.LoadLevel(SceneDefinitions.InsideScoria);
    }
}
