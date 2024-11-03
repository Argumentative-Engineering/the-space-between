using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScoriaDoor : GameInteractable
{
    FixScoriaScenario _scoriaScenario;

    void Start()
    {
        _scoriaScenario = ScenarioManager.Instance.GetScenario<FixScoriaScenario>();
    }

    public override bool TryInteract()
    {
        if (!_scoriaScenario.IsScoriaWorking())
        {
            transform.DOShakePosition(0.5f, 0.05f);

            if (_scoriaScenario.PowerCellConnectionCount < 2)
                print("SCORIA: insufficient power");
            else if (!_scoriaScenario.AreFuelLinesFixed)
                print("SCORIA: fuel lines damaged");

            return false;
        }
        return base.TryInteract();
    }

}
