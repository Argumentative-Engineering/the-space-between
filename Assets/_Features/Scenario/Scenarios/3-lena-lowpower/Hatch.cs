using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Hatch : DialogueInteractable
{
    [Header("References")]
    [SerializeField] Scenario _scenario;
    [SerializeField] DialogueData _hasntRemovedItemsDialogue, _noSuitDialogue, _finallyDialogue;
    [SerializeField] PlayableDirector _director;

    bool _sayFinally;

    void Update()
    {
        if (_scenario.ScenarioKeys.ContainsKey("removed-items") && _scenario.ScenarioKeys.ContainsKey("wearing-spacesuit") && !_sayFinally)
        {
            _sayFinally = true;
            SayFinally();
        }
    }

    void SayFinally()
    {
        NarrativeManager.Instance.PlayDialogue(_finallyDialogue);
    }

    public override bool TryInteract()
    {
        if (!_scenario.ScenarioKeys.ContainsKey("removed-items"))
        {
            NarrativeManager.Instance.PlayDialogue(_hasntRemovedItemsDialogue);
            return true;
        }

        if (!_scenario.ScenarioKeys.ContainsKey("wearing-spacesuit"))
        {
            NarrativeManager.Instance.PlayDialogue(_noSuitDialogue);
            return true;
        }

        if (!base.TryInteract())
        {
            CutsceneManager.Instance.RunCutscene(_director,
            () => GameManager.Instance.LoadLevel(SceneDefinitions.HelenExterior));
        }

        return false;
    }
    public void DisablePlayer()
    {
        GameManager.Instance.Player.GetComponent<PlayerSettings>().IsFrozen = true;
    }
}
