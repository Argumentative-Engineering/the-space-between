using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellLookAt : MonoBehaviour
{
    [SerializeField] FixScoriaScenario _scen;

    void Update()
    {
        if (!_scen.CanLookAtPowerCell || _scen.LookedAtPowerCell) return;

        var dot = Vector3.Dot(Camera.main.transform.forward, transform.position - Camera.main.transform.position);
        if (dot < 20 && dot > 0)
        {
            EventManager.Instance.BroadcastEvent("look-at-power-cell", transform.position);
        }
    }
}
