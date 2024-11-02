using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBlockedScenario : Scenario
{
    [SerializeField] Transform _evaKitTether;

    private void Start()
    {
        EventManager.Instance.RegisterListener("on-tether", OnTether);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("on-tether", OnTether);
    }

    private void OnTether(object[] obj)
    {
        var tether = (Transform)obj[0];
        if (tether != _evaKitTether) return;

        _evaKitTether.transform.parent = null;
        _evaKitTether.GetComponent<Rigidbody>().isKinematic = false;
        _evaKitTether.GetComponent<Rigidbody>().AddForce(_evaKitTether.forward + Vector3.down * 5, ForceMode.Impulse);
    }

    public override IEnumerator RunScenario()
    {
        yield return base.RunScenario();
    }
}
