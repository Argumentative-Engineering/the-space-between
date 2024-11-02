using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _jumpForce = 10;

    [Header("References")]
    [SerializeField] PlayerSettings _settings;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] PlayerLocalInput _input;
    [SerializeField] Light _flashlight;

    private void Update()
    {
        float targFov = _input.IsZooming ? 30 : 60;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targFov, 10 * Time.deltaTime);

        if (_settings.IsFrozen)
        {
            _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, 10 * Time.deltaTime);
            return;
        }
        if (_settings.LookClamp.x != 0)
            _input.RotationVector.x = Mathf.Clamp(_input.RotationVector.x, -_settings.LookClamp.x, _settings.LookClamp.x);

        _input.RotationVector.y = Mathf.Clamp(_input.RotationVector.y, -_settings.LookClamp.y, _settings.LookClamp.y);

        _rigidbody.angularVelocity = Vector3.zero;
        if (_settings.OverrideCameraRotation)
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
        _rigidbody.AddForce(vec, ForceMode.Acceleration);
    }

    public void Jump()
    {
        if (!_settings.IsAnchored) return;

        _rigidbody.AddForce(Camera.main.transform.forward * _jumpForce, ForceMode.Impulse);
        _settings.IsAnchored = false;
        GetComponent<PlayerInteraction>().IsInteracting = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out GrabbableObject grabbable))
        {
            _rigidbody.velocity = _rigidbody.velocity * 0.5f;
        }
    }
}
