using System.Collections;
using NaughtyAttributes.Test;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerTether : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask _interactionMask;
    [SerializeField] float _pullForce = 5;
    [SerializeField] float _tetherMaxLength = 20;

    [Header("References")]
    [SerializeField] PlayerLocalInput _input;
    [SerializeField] Transform _pullFrom;
    [SerializeField] Transform _tether;
    [SerializeField] Renderer[] _renderers;

    bool _isThrown = false;
    bool _canPull = false;
    GameObject _connectedObject;
    Rigidbody _tetherRb;
    Vector3 _tetherDir;
    Rigidbody _rb;
    float _dist;

    private void Start()
    {
        _rb = GameManager.Instance.Player.GetComponent<Rigidbody>();
        _tetherRb = _tether.GetComponent<Rigidbody>();
        _tether.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!_isThrown)
        {
            _tether.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
            return;
        }

        if (_connectedObject == null && Physics.Raycast(_tether.transform.position, _tether.transform.forward, out RaycastHit hit, 0.2f, _interactionMask))
        {
            _connectedObject = hit.collider.gameObject;
            _tetherRb.rotation = Quaternion.LookRotation(-hit.normal);
            _tetherRb.angularVelocity = Vector3.zero;
            _tetherRb.velocity = Vector3.zero;
            _canPull = true;
            _connectedObject.GetComponent<FixedJoint>().connectedBody = _tetherRb;
        }
        else
        {
            if (_dist > 4) _canPull = true;
        }

        _dist = Vector3.Distance(_tether.position, Camera.main.transform.position);
        _tetherDir = _tether.position - Camera.main.transform.position;

        if (_dist > _tetherMaxLength)
        {
            _tetherRb.velocity = Vector3.zero;
            _tetherRb.position = Camera.main.transform.position + _tetherDir.normalized * _tetherMaxLength;
        }
        else if (_dist <= 4 && _canPull)
        {
            _tetherRb.drag = 20;
            if (_connectedObject != null)
            {
                _connectedObject.transform.parent = null;
                _connectedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _connectedObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                _connectedObject = null;
            }
            _tetherRb.drag = 0;
            ShowHeld(true);
            _tether.gameObject.SetActive(false);
            _canPull = false;
            _isThrown = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_canPull) return;

        if (_input.IsFiring)
        {
            _tetherRb.AddForce(_pullForce * -_tetherDir);
        }
    }

    public void ThrowTether()
    {
        if (_isThrown || !gameObject.activeSelf) return;

        ShowHeld(false);
        _tether.gameObject.SetActive(true);
        _tetherRb.angularVelocity = Vector3.zero;
        _tetherRb.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        _tetherRb.velocity = Vector3.zero;
        _tetherRb.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
        _isThrown = true;


        _tetherRb.AddForce(Camera.main.transform.forward * 500);
    }

    void ShowHeld(bool visible)
    {
        foreach (var renderer in _renderers) renderer.enabled = visible;
    }
}