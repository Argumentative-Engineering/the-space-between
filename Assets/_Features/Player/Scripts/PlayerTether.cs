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
        if (!_isThrown) return;
        if (_connectedObject == null && Physics.Raycast(_tether.transform.position, _tether.transform.forward, out RaycastHit hit, 0.2f, _interactionMask))
        {
            _connectedObject = hit.collider.gameObject;
            _tetherRb.rotation = Quaternion.LookRotation(-hit.normal);
            _tetherRb.angularVelocity = Vector3.zero;
            _tetherRb.velocity = Vector3.zero;
            _connectedObject.GetComponent<FixedJoint>().connectedBody = _tetherRb;
        }

        _tetherDir = _tether.position - Camera.main.transform.position;
        if (_tetherDir.magnitude > _tetherMaxLength && _tetherDir.magnitude < 100)
        {
            _tetherRb.velocity = Vector3.zero;
            _tetherRb.position = Camera.main.transform.position + _tetherDir.normalized * _tetherMaxLength;
        }

        _dist = Mathf.Abs(_tetherDir.magnitude);
        if (_dist > 4 && _dist < 100) _canPull = true;
        if (_dist <= 4 && _canPull)
        {
            _tetherRb.velocity = Vector3.zero;
            if (_connectedObject != null)
            {
                _connectedObject.transform.parent = null;
                _connectedObject = null;
            }

            _tether.gameObject.SetActive(false);
            _canPull = false;
            _isThrown = false;
        }
    }
    private void FixedUpdate()
    {
        if (!_isThrown) return;
        if (!_canPull) return;

        if (_input.IsFiring)
        {
            _tetherRb.AddForce(_pullForce * -_tetherDir);
            if (_connectedObject != null)
                _rb.AddForce(_pullForce * _tetherDir);
        }
        else
        {
            if (_tetherRb.velocity.magnitude >= 0.5f)
            {
                _tetherRb.velocity = Vector3.Lerp(_tetherRb.velocity, new Vector3(_tetherRb.velocity.x, _tetherRb.velocity.y, 0), 10);
            }
        }
    }

    public void ThrowTether()
    {
        if (_isThrown || !gameObject.activeSelf) return;

        _tether.gameObject.SetActive(true);
        _tetherRb.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
        _isThrown = true;

        _tetherRb.AddForce(Camera.main.transform.forward * 100);
        _tetherRb.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}