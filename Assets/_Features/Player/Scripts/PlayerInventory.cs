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

    [SerializeField] GameObject[] _items;
    public GameObject EquippedItem { get; private set; }
    PlayerSettings _settings;

    void Start()
    {
        _settings = PlayerSettings.Instance;
        foreach (var item in _items)
        {
            item.SetActive(false);
        }
    }

    public void EquipItem(InventoryItems item)
    {
        EquipItem((int)item);
    }

    public void EquipItem(int itemIndex)
    {
        // bruteforced lol but its ok we only have 2 items
        if (itemIndex == 0 && !_settings.CanUseThrusters) return;
        if (itemIndex == 1 && !_settings.CanUseTether) return;

        for (int i = 0; i < _items.Length; i++)
        {
            if (i == itemIndex)
            {
                _items[i].SetActive(true);
                SetVisible(i, true);
                EquippedItem = _items[i];
                continue;
            }
            SetVisible(i, false);
        }

    }

    void SetVisible(int itemIndex, bool visible)
    {
        var item = _items[itemIndex].transform;
        item.gameObject.SetActive(visible);
    }
}