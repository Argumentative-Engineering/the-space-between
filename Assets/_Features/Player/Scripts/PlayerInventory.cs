using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public enum InventoryItems
{
    Thruster = 0,
    Tether = 1
}

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] InventoryItem[] _items;
    public InventoryItem EquippedItem { get; private set; }
    PlayerSettings _settings;

    void Start()
    {
        _settings = PlayerSettings.Instance;
        foreach (var item in _items)
        {
            item.gameObject.SetActive(false);
        }

        // DequipAll();
    }

    public void EquipItem(InventoryItems item)
    {
        EquipItem((int)item);
    }

    public void DequipAll()
    {
        foreach (var item in _items)
        {
            item.Dequip();
        }
    }

    public void EquipItem(int itemIndex)
    {
        DequipAll();

        if (itemIndex == 0 && !_settings.CanUseThrusters) return;
        if (itemIndex == 1 && !_settings.CanUseTether) return;

        for (int i = 0; i < _items.Length; i++)
        {
            if (i == itemIndex)
            {
                EquippedItem = _items[i];
                EquippedItem.gameObject.SetActive(true);
                EquippedItem.Equip(true);
                continue;
            }
            _items[i].gameObject.SetActive(false);
        }
    }
}