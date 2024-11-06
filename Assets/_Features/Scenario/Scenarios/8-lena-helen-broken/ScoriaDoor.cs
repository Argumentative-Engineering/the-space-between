using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScoriaDoor : GameInteractable
{
    [SerializeField] FixScoriaScenario _scoriaScenario;

    bool _interactedOnce = false;
    [SerializeField] DialogueData _insufficientPower, _insufficientNoLena;

    public override bool TryInteract()
    {
        if (!_scoriaScenario.IsScoriaWorking())
        {
            transform.DOShakePosition(0.5f, 0.05f);

            if (_scoriaScenario.PowerCellConnectionCount < 2)
                NarrativeManager.Instance.PlayDialogue(_interactedOnce ? _insufficientNoLena : _insufficientPower);
            if (!_interactedOnce) _interactedOnce = true;
            return false;
        }

        _scoriaScenario.Complete();
        return base.TryInteract();
    }

}
