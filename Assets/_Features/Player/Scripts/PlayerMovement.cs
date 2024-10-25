using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _moveSpeed = 5;

    [Header("References")]
    [SerializeField] PlayerSettings _settings;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] PlayerLocalInput _input;

    private void Update()
    {
        if (_settings.LookClamp.x != 0)
            _input.RotationVector.x = Mathf.Clamp(_input.RotationVector.x, -_settings.LookClamp.x, _settings.LookClamp.x);

        _input.RotationVector.y = Mathf.Clamp(_input.RotationVector.y, -_settings.LookClamp.y, _settings.LookClamp.y);

        _rigidbody.angularVelocity = Vector3.zero;
        Camera.main.transform.rotation = Quaternion.Euler(_input.RotationVector.y, _input.RotationVector.x, 0);
    }

    void FixedUpdate()
    {
        var vec = (Camera.main.transform.forward * _input.MoveVector.y + Camera.main.transform.right * _input.MoveVector.x).normalized * _moveSpeed;
        _rigidbody.AddForce(vec);
    }
}
