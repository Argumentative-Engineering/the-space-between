using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerLocalInput : MonoBehaviour
{
    [HideInInspector] public Vector2 MoveVector;
    [HideInInspector] public Vector2 RotationVector;

    [SerializeField] PlayerSettings _settings;

    public UnityEvent OnInteract;

    GameInput _input;
    private void Awake()
    {
        _input = new GameInput();
    }

    private void OnEnable()
    {
        _input.Player.Move.performed += OnMovePerformed;
        _input.Player.Move.canceled += OnMoveCanceled;
        _input.Player.Interact.performed += OnUsePerformed;
        _input.Player.Look.performed += OnLookPerformed;
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= OnMovePerformed;
        _input.Player.Move.canceled -= OnMoveCanceled;
        _input.Player.Interact.performed -= OnUsePerformed;
        _input.Player.Look.performed -= OnLookPerformed;
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

    public void SnapToRotation(Quaternion rotation)
    {
        RotationVector.x = rotation.eulerAngles.y;
        RotationVector.y = rotation.eulerAngles.x;
    }

    public void SnapToRotation(Vector3 eulerAngles)
    {
        RotationVector = eulerAngles;
    }
}