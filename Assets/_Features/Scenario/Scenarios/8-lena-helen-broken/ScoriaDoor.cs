using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScoriaDoor : GameInteractable
{
    FixScoriaScenario _scoriaScenario;
    [SerializeField] DialogueData _insufficientPower;

    void Start()
    {
    }

    public override bool TryInteract()
    {
        _scoriaScenario = ScenarioManager.Instance.GetScenario<FixScoriaScenario>();
        if (!_scoriaScenario.IsScoriaWorking())
        {
            transform.DOShakePosition(0.5f, 0.05f);

            if (_scoriaScenario.PowerCellConnectionCount < 2)
                NarrativeManager.Instance.PlayDialogue(_insufficientPower);
            else if (!_scoriaScenario.AreFuelLinesFixed)
                print("SCORIA: fuel lines damaged");

            return false;
        }

        _scoriaScenario.Complete();
        return base.TryInteract();
    }

}
