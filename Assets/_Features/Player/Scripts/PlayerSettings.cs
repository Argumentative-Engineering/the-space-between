using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [Header("Gameplay")]
    [Range(0, 10)]
    public float MouseSensitivity;
    public Vector2 LookClamp;
    public bool IsFrozen;
    [field: SerializeField] public PlayerMovementSettingsData PlayerMovementSettings { get; private set; }

    [Header("References")]
    [SerializeField] Rigidbody _rb;

    public void UpdateSettings(PlayerMovementSettingsData settings)
    {
        PlayerMovementSettings = settings;
        _rb.useGravity = PlayerMovementSettings.UseGravity;
    }
}