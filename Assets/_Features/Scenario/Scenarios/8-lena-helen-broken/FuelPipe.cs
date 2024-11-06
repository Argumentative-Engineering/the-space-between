using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPipe : GameInteractable
{
    [SerializeField] bool _isProperPipe;
    [SerializeField] DialogueData _dialogue;
    [SerializeField] FixScoriaScenario _scn;

    public override bool TryInteract()
    {
        // EventManager.Instance.BroadcastEvent(EventDefinitions.CheckPipe, _isProperPipe, gameObject);
        NarrativeManager.Instance.PlayDialogue(_dialogue);

        if (_isProperPipe)
        {
            _scn.HasFuelPipe = true;
            Destroy(gameObject);
        }
        return base.TryInteract();
    }
}
