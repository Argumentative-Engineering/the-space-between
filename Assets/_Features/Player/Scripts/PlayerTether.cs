using NaughtyAttributes.Test;
using UnityEngine;

public class PlayerTether : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask _interactionMask;
    [SerializeField] float _pullForce = 5;

    [Header("References")]
    [SerializeField] PlayerLocalInput _input;
    [SerializeField] Transform _pullFrom;
    [SerializeField] Transform _tether;

    bool _isThrown = false;
    GameObject _connectedObject;
    Rigidbody _tetherRb;

    private void Start()
    {
        _tetherRb = _tether.GetComponent<Rigidbody>();
        _tether.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_isThrown)
        {

            if (_connectedObject == null && Physics.Raycast(_tether.transform.position, _tether.transform.forward, out RaycastHit hit, 0.2f, _interactionMask))
            {
                _connectedObject = hit.collider.gameObject;
                _connectedObject.GetComponent<Rigidbody>().isKinematic = true;
                _connectedObject.transform.parent = _tether;
                _tetherRb.velocity = Vector3.zero;
            }

            var playerDir = _tether.position - Camera.main.transform.position;
            if (playerDir.magnitude > 5 && playerDir.magnitude < 100)
            {
                _tetherRb.velocity = Vector3.zero;
                _tetherRb.position = Camera.main.transform.position + playerDir.normalized * 5;
            }
        }
    }
    private void FixedUpdate()
    {
        if (_input.IsFiring && _connectedObject != null)
        {
            _tetherRb.velocity = _pullForce * Time.deltaTime * -_tether.forward;

            if (Vector3.Distance(_tether.position, _pullFrom.position) < 2)
            {

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