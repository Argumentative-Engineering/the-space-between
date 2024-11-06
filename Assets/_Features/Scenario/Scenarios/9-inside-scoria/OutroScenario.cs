using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroScenario : Scenario
{
    [SerializeField] StudioEventEmitter _beep;
    [SerializeField] DialogueData _ohMack;

    public override IEnumerator RunScenario()
    {
        yield return new WaitForSeconds(5);
        _beep.Play();
        yield return base.RunScenario();
    }

    public void TalkToMack()
    {
        _beep.Stop();
        StartCoroutine(EndTheGame());
    }

    IEnumerator EndTheGame()
    {
        NarrativeManager.Instance.PlayDialogue(_ohMack);
        yield return new WaitForSeconds(6);
        CutsceneManager.Instance.Fade(1, () =>
        {
            SceneManager.LoadSceneAsync(SceneDefinitions.Outro);
        }, duration: 3);
    }
}
