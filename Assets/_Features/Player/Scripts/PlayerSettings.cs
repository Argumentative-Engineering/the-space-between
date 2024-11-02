using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum KillType
{
    NoOxygen,
    LostInSpace
}

public class PlayerSettings : MonoBehaviour
{
    [Header("Gameplay")]
    [Range(0, 10)]
    public float MouseSensitivity;
    public Vector2 LookClamp;
    public bool IsFrozen;
    public bool OverrideCameraRotation;
    public bool IsAnchored = false;

    [Header("Inventory Settings")]
    public bool CanUseThrusters = false;
    public bool CanUseTether = false;

    [field: SerializeField] public PlayerMovementSettingsData PlayerMovementSettings { get; private set; }

    [Header("References")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] GameObject[] _ui;
    [SerializeField] GameObject[] _helmet;

    public static PlayerSettings Instance { get; private set; }
    private void Awake() => Instance = this;

    public void UpdateSettings(PlayerMovementSettingsData settings)
    {
        PlayerMovementSettings = settings;
        _rb.useGravity = PlayerMovementSettings.UseGravity;
        _rb.drag = PlayerMovementSettings.Drag;
        foreach (var helmet in _helmet)
        {
            helmet.SetActive(settings.ShowHelmet);
        }

        SetUIVisiblity(settings.ShowHelmet);
    }

    public void KillPlayer(KillType type)
    {
        if (GameManager.Instance.IsReloading) return;
        IsFrozen = true;
        print("Player died from " + type.ToString());
        SetUIVisiblity(false);
        StartCoroutine(Kill());
    }

    public void SetUIVisiblity(bool visible)
    {
        foreach (var ui in _ui) ui.SetActive(visible);
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(2);
        CutsceneManager.Instance.Fade(2, null);
        // GameManager.Instance.LoadLevel(SceneManager.GetActiveScene().name);
    }

    public static void FreezePlayer(bool isFrozen)
    {
        Instance.IsFrozen = isFrozen;
    }
}