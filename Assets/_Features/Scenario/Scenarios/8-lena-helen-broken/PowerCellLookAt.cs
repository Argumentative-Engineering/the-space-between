using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellLookAt : MonoBehaviour
{
    [SerializeField] FixScoriaScenario _scen;

    void Update()
    {
        if (!_scen.CanLookAtPowerCell || _scen.LookedAtPowerCell) return;

        var angle = Vector3.Angle(Camera.main.transform.forward, transform.position - Camera.main.transform.position);
        if (angle < 10 && angle > 0)
        {
            EventManager.Instance.BroadcastEvent("look-at-power-cell", transform.position);
        }
    }
}
