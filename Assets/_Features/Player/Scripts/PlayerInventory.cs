using DG.Tweening;
using FMOD;
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
    [SerializeField] Transform _offhandPosition;
    public InventoryItem EquippedItem { get; private set; }
    public GameObject CurrentOffhandItem { get; private set; }
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
            item.gameObject.SetActive(false);
        }
        EquippedItem = null;
    }

    public void EquipItem(int itemIndex, bool forceEquip = false)
    {
        // DequipAll();

        if (itemIndex == 0 && !_settings.CanUseThrusters) return;
        if (itemIndex == 1 && !_settings.CanUseTether) return;

        if (EquippedItem == _items[itemIndex])
        {
            EquippedItem.Dequip();
            EquippedItem = null;
            return;
        }

        for (int i = 0; i < _items.Length; i++)
        {
            if (i == itemIndex)
            {
                EquippedItem = _items[i];
                EquippedItem.gameObject.SetActive(true);
                EquippedItem.Equip(true, forceEquip);
                continue;
            }
            _items[i].gameObject.SetActive(false);
        }
    }

    public void EquipOffhand(GameObject offhand)
    {
        if (CurrentOffhandItem != null)
        {
            CurrentOffhandItem.transform.parent = null;
            CurrentOffhandItem.transform.position = Camera.main.transform.forward;
            CurrentOffhandItem.GetComponent<Rigidbody>().isKinematic = true;
        }

        CurrentOffhandItem = offhand;
        CurrentOffhandItem.transform.parent = Camera.main.transform;
        CurrentOffhandItem.GetComponent<Rigidbody>().isKinematic = true;
        CurrentOffhandItem.transform.position = _offhandPosition.position;
    }
}