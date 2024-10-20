using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _thrustPower = 5;
    [SerializeField] float _mouseSensitivity = 1;

    [Header("References")]
    [SerializeField] Rigidbody _rigidbody;

    GameInput _input;

    Vector3 _moveVector;
    Vector2 _rotationVec;

    private void Awake()
    {
        _input = new GameInput();
    }

    private void OnEnable()
    {
        _input.Player.Move.performed += OnMovePerformed;
        _input.Player.Move.canceled += OnMoveCanceled;
        _input.Player.Thrust.performed += OnThrustPerformed;
        _input.Player.Thrust.canceled += OnThrustCanceled;
        _input.Player.Look.performed += OnLookPerformed;
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= OnMovePerformed;
        _input.Player.Move.canceled -= OnMoveCanceled;
        _input.Player.Thrust.performed -= OnThrustPerformed;
        _input.Player.Thrust.canceled -= OnThrustCanceled;
        _input.Player.Look.performed -= OnLookPerformed;
        _input.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveVector = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
        => _moveVector = Vector3.zero;

    private void OnThrustPerformed(InputAction.CallbackContext context)
    {
        _moveVector.z = context.ReadValue<float>();
    }

    private void OnThrustCanceled(InputAction.CallbackContext context)
        => _moveVector.z = 0;

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        _rotationVec.x += context.ReadValue<Vector2>().x * _mouseSensitivity;
        _rotationVec.y -= context.ReadValue<Vector2>().y * _mouseSensitivity;

        _rotationVec.y = Mathf.Clamp(_rotationVec.y, -90, 90);
    }

    private void Update()
    {
        _rigidbody.rotation = Quaternion.Euler(0, _rotationVec.x, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(_rotationVec.y, 0, 0);
    }

    void FixedUpdate()
    {
        var vec = (transform.forward * _moveVector.y + transform.right * _moveVector.x + transform.up * _moveVector.z).normalized;
        vec.y *= _thrustPower;
        _rigidbody.AddForce(vec);
    }
}
