using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenaPushPuzzleScenario : Scenario
{
    public override IEnumerator RunScenario()
    {
        Announcement.Instance.ShowAnnouncment("Low oxygen");
        GameManager.Instance.Player.GetComponent<Rigidbody>().drag = 0;
        return base.RunScenario();
    }

    public void WithCommandModule()
    {
        ScenarioManager.Instance.RunNextScenario();
    }
}
