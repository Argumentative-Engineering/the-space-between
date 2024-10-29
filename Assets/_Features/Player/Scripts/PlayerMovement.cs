using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerSettings _settings;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] PlayerLocalInput _input;

    private void Update()
    {
        if (_settings.IsFrozen)
        {
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, 10 * Time.deltaTime);
            return;
        }
        if (_settings.LookClamp.x != 0)
            _input.RotationVector.x = Mathf.Clamp(_input.RotationVector.x, -_settings.LookClamp.x, _settings.LookClamp.x);

        _input.RotationVector.y = Mathf.Clamp(_input.RotationVector.y, -_settings.LookClamp.y, _settings.LookClamp.y);

        _rigidbody.angularVelocity = Vector3.zero;
        if (_settings.UseLocalRot)
            Camera.main.transform.localRotation = Quaternion.Euler(_input.RotationVector.y, _input.RotationVector.x, 0);
        else
            Camera.main.transform.rotation = Quaternion.Euler(_input.RotationVector.y, _input.RotationVector.x, 0);
    }

    void FixedUpdate()
    {
        if (_settings.IsFrozen) return;
        var fwd = Camera.main.transform.forward;
        var right = Camera.main.transform.right;

        if (_settings.PlayerMovementSettings.UseGravity)
            fwd.y = right.y = 0;

        var vec = (fwd * _input.MoveVector.y + right * _input.MoveVector.x).normalized * _settings.PlayerMovementSettings.MoveSpeed;
        _rigidbody.AddForce(vec);
    }
}
