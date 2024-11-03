using System;
using System.Collections;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;

public class CanadarmController : GameInteractable
{
    bool _isActive;
    [SerializeField] Camera[] _canadarmCameras;
    Camera _currentCamera;
    [SerializeField] Transform _canadarmTip;

    PlayerLocalInput _input;
    int _currCameraIndex;
    Rigidbody _rb;

    Sequence _dropSeq;

    bool _canLeave = false;

    private void Start()
    {
        _currentCamera = _canadarmCameras[0];

        _rb = _canadarmTip.GetComponent<Rigidbody>();

        _input = PlayerSettings.Instance.GetComponent<PlayerLocalInput>();
        _input.OnSwitchCamera.AddListener(SwitchCamera);
        _input.OnJump.AddListener(Drop);
        _input.OnInteract.AddListener(() =>
        {
            if (_canLeave)
                Leave();
        });
    }

    private void Drop()
    {
        if (!_isActive || (_dropSeq != null && _dropSeq.IsPlaying())) return;
        var initialY = _canadarmTip.localPosition.y;

        _isActive = false;
        _dropSeq = DOTween.Sequence()
            .Append(_canadarmTip.DOLocalMoveY(-2, 2))
            .AppendInterval(1)
            .Append(_canadarmTip.DOLocalMoveY(initialY, 2))
            .AppendCallback(() => _isActive = true);
    }

    public override bool TryInteract()
    {
        _isActive = true;
        _currentCamera.gameObject.SetActive(true);
        base.TryInteract();

        PlayerSettings.FreezePlayer(true);
        return true;
    }

    public void Leave()
    {
        if (!_isActive) return;
        _canLeave = true;
        _currentCamera.gameObject.SetActive(false);
        PlayerSettings.FreezePlayer(false);
        PlayerSettings.Instance.GetComponent<PlayerInteraction>().StopInteracting();
        _isActive = false;
    }

    void SwitchCamera(int index)
    {
        if (!_isActive) return;
        _currCameraIndex = Mathf.Abs((_currCameraIndex + index) % _canadarmCameras.Length);
        _currentCamera.gameObject.SetActive(false);
        _currentCamera = _canadarmCameras[_currCameraIndex];
        _currentCamera.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (!_isActive) return;
        var fwd = _currentCamera.transform.forward;
        var right = _currentCamera.transform.right;
        fwd.y = right.y = 0;

        var move = (fwd * _input.MoveVector.y + right * _input.MoveVector.x).normalized;
        _rb.AddForce(move * 0.2f, ForceMode.VelocityChange);

        if (_rb.velocity.magnitude > 5)
            _rb.velocity = _rb.velocity.normalized * 5;
    }
}
