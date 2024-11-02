using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanPanels : DialogueTrigger
{
    protected override void Trigger()
    {
        PlayerInventory.Instance.EquipItem(InventoryItems.Thruster);
    }
}
