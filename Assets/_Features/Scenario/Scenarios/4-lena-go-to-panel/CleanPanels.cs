using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanPanels : DialogueTrigger
{
    protected override void Trigger()
    {
        PlayerSettings.Instance.CanUseThrusters = true;
        GameHints.Instance.ShowHint("Press 1 to equip Thrusters");
        // PlayerInventory.Instance.EquipItem(InventoryItems.Thruster);
    }
}
