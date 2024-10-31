using System;
using System.Collections;
using UnityEngine;

public class GetLFSScenario : Scenario
{
    void Start()
    {
        EventManager.Instance.RegisterListener("tether-object", OnTetherObject);
    }

    private void OnTetherObject(object[] obj)
    {
        GameObject tethered = (GameObject)obj[0];
        if (tethered.name == "LFS")
        {
            print("got lfs!");
        }
    }
}
