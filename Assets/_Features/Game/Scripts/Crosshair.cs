using UnityEngine;
using UnityEngine.InputSystem.Switch;
using UnityEngine.UI;

public enum CrosshairType
{
    Normal,
    Grab,
    Push
}

public class Crosshair : MonoBehaviour
{
    [Header("Textures")]
    [SerializeField] Sprite _normal;
    [SerializeField] Sprite _grab;
    [SerializeField] Sprite _push;

    [Header("References")]
    [SerializeField] Image _crosshair;

    public static Crosshair Instance { get; private set; }
    private void Awake() => Instance = this;

    private void Start()
    {
        SetCrosshair(CrosshairType.Normal);
    }

    public void SetCrosshair(CrosshairType type)
    {
        var crosshair = type switch
        {
            CrosshairType.Grab => (_grab, 0.5f),
            CrosshairType.Push => (_push, 1f),
            _ => (_normal, 0.1f)
        };

        _crosshair.sprite = crosshair.Item1;
        _crosshair.transform.localScale = Vector3.one * crosshair.Item2;
    }

}