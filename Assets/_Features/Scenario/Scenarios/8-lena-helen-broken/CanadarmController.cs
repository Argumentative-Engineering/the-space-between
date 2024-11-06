using System;
using System.Collections;
using System.Diagnostics;
using DG.Tweening;
using FMODUnity;
using UnityEngine;

public class CanadarmController : GameInteractable
{
    bool _isActive;
    [SerializeField] Camera[] _canadarmCameras;
    Camera _currentCamera;
    [SerializeField] Transform _canadarmTip;

    [SerializeField] DialogueData _startupDialogue, _successDialogue, _failDialogue, _backToHatchDialogue;
    [SerializeField] StudioEventEmitter _sound;

    PlayerLocalInput _input;
    int _currCameraIndex;
    Rigidbody _rb;

    Sequence _dropSeq;

    bool _saidVO = false;

    bool _canLeave = false;

    private void Start()
    {
        EventManager.Instance.RegisterListener("canadarm-on", OnCanadarmOn);
        Tooltip = "Controller of some sort";
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

    private void OnDisable()
    {
        EventManager.Instance.UnregisterListener("canadarm-on", OnCanadarmOn);
    }

    private void OnCanadarmOn(object[] obj)
    {
        _sound.Play();
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

        StartCoroutine(_canadarmTip.GetComponent<CanadarmTip>().WaitDialogue(_successDialogue, _failDialogue));
    }

    public override bool TryInteract()
    {
        PlayerInteraction.Instance.StopInteracting();
        if (!_saidVO)
        {
            StartCoroutine(Startup());
            base.TryInteract();
            return true;
        }

        Tooltip = "Canadarm";
        _isActive = true;
        _currentCamera.gameObject.SetActive(true);
        base.TryInteract();

        PlayerSettings.FreezePlayer(true);
        return true;
    }

    IEnumerator Startup()
    {
        _saidVO = true;
        PlayerSettings.FreezePlayer(true);
        NarrativeManager.Instance.PlayDialogue(_startupDialogue);
        yield return new WaitForSeconds(8);
        Tooltip = "Canadarm";
        _isActive = true;
        _currentCamera.gameObject.SetActive(true);
    }

    public void LeaveDialogue()
    {
        NarrativeManager.Instance.PlayDialogue(_backToHatchDialogue);
    }

    public void Leave()
    {
        if (!_isActive) return;
        _sound.Stop();
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
