using System;
using System.Collections;
using UnityEngine;

public class GetLFSScenario : Scenario
{
    void Start()
    {
        EventManager.Instance.RegisterListener("tether-object", OnTetherObject);
        EventManager.Instance.RegisterListener("got-lfs", GotLfs);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("tether-object", OnTetherObject);
        EventManager.Instance.UnregisterListener("got-lfs", GotLfs);
    }

    private void GotLfs(object[] obj)
    {
        Announcement.Instance.StopAnnouncement();
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
