using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanPanels : DialogueTrigger
{
    protected override void Trigger()
    {
        GameManager.Instance.Player.GetComponent<PlayerThruster>().SetThrusterVisiblity(true);
    }
}
