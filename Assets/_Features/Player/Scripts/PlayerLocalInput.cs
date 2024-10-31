using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerLocalInput : MonoBehaviour
{
    [HideInInspector] public Vector2 MoveVector;
    [HideInInspector] public Vector2 RotationVector;

    public UnityEvent OnInteract;
    public UnityEvent OnFire;

    public bool IsFiring { get; private set; }
    PlayerSettings _settings;

    GameInput _input;
    private void Awake()
    {
        _input = new GameInput();
    }

    void Start()
    {
        _settings = PlayerSettings.Instance;
    }

    private void OnEnable()
    {
        _input.Player.Move.performed += OnMovePerformed;
        _input.Player.Move.canceled += OnMoveCanceled;
        _input.Player.Interact.performed += OnUsePerformed;
        _input.Player.Fire.performed += OnFirePerformed;
        _input.Player.Fire.canceled += OnFireCanceled;
        _input.Player.Look.performed += OnLookPerformed;
        _input.Player.Inventory.performed += OnInventoryPerformed;
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Player.Move.performed -= OnMovePerformed;
        _input.Player.Move.canceled -= OnMoveCanceled;
        _input.Player.Interact.performed -= OnUsePerformed;
        _input.Player.Look.performed -= OnLookPerformed;
        _input.Player.Inventory.performed -= OnInventoryPerformed;
        _input.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context) => MoveVector = context.ReadValue<Vector2>();
    private void OnMoveCanceled(InputAction.CallbackContext context) => MoveVector = Vector3.zero;

    private void OnUsePerformed(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        var rot = context.ReadValue<Vector2>();
        RotationVector.x += rot.x * (_settings.MouseSensitivity / 100);
        RotationVector.y -= rot.y * (_settings.MouseSensitivity / 100);
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        OnFire?.Invoke();
        IsFiring = true;
    }

    private void OnFireCanceled(InputAction.CallbackContext context) => IsFiring = false;

    private void OnInventoryPerformed(InputAction.CallbackContext context)
    {
        var val = context.ReadValue<float>();

        PlayerInventory.Instance.EquipItem((int)val);
    }



    public void SnapToRotation(Quaternion rotation)
    {
        RotationVector.x = rotation.eulerAngles.y;
        RotationVector.y = rotation.eulerAngles.x;
    }
}