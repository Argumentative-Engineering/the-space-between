using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerThruster : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask _interactionMask;
    [field: SerializeField] public float ThrusterRange { get; private set; } = 5;

    [Header("References")]
    [SerializeField] GameObject _thrusters;
    [SerializeField] PlayerLocalInput _input;
    [SerializeField] Rigidbody _rb;
    public bool IsShowing { get; private set; } = false;

    Vector3 _startPos;

    EventManager _evt;

    void Start()
    {
        _evt = EventManager.Instance;
        _startPos = _thrusters.transform.localPosition;
        _thrusters.SetActive(false);
        SetThrusterVisiblity(false);
    }

    void Update()
    {
        if (!IsShowing || !_input.IsFiring) return;

        // lazy to rewrite to have generic player thruster so i'll just use events.
        // performance? idk lol dont care anymore
        // laziest piece of code ever
        _evt.BroadcastEvent("player-thrust", this, _interactionMask);
        _rb.AddForce(-Camera.main.transform.forward * 0.5f);
    }

    public void SetThrusterVisiblity(bool visiblity)
    {
        IsShowing = visiblity;
        _thrusters.SetActive(visiblity);

        var targPos = visiblity ? _startPos : _startPos + Vector3.back;
        _thrusters.transform.DOLocalMove(targPos, 2f);
    }
}
