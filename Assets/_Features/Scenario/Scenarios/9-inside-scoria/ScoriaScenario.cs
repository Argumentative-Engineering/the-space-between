using System;
using System.Collections;
using UnityEngine;

public class ScoriaScenario : Scenario
{
    [SerializeField] Transform _hatch;

    [Header("Dialogues")]
    [SerializeField] DialogueData _getUpThere;
    private void Start()
    {
        EventManager.Instance.RegisterListener("look-hatch", OnLookHatch);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("look-hatch", OnLookHatch);
    }

    private void OnLookHatch(object[] obj)
    {
        AnimationUtils.AnimateLookAt(_hatch.position, duration: 1, delay: 4);
    }

    public override IEnumerator RunScenario()
    {
        CutsceneManager.Instance.Fade(0, () => NarrativeManager.Instance.PlayDialogue(_getUpThere), startBlack: true);
        return base.RunScenario();
    }
}