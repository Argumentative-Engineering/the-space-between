using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [Header("Gameplay")]
    [Range(0, 10)]
    public float MouseSensitivity;
    public Vector2 LookClamp;
    public bool IsFrozen;
    public bool UseLocalRot;

    [Header("Inventory Settings")]
    public bool CanUseThrusters = false;
    public bool CanUseTether = false;

    [field: SerializeField] public PlayerMovementSettingsData PlayerMovementSettings { get; private set; }

    [Header("References")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] GameObject _helmet;

    public static PlayerSettings Instance { get; private set; }
    private void Awake() => Instance = this;

    public void UpdateSettings(PlayerMovementSettingsData settings)
    {
        PlayerMovementSettings = settings;
        _rb.useGravity = PlayerMovementSettings.UseGravity;
        _rb.drag = PlayerMovementSettings.Drag;
        _helmet.SetActive(settings.ShowHelmet);
    }
}