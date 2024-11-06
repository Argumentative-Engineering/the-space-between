using System.Collections;
using DG.Tweening;
using FMODUnity;
using UnityEngine;

public class PlayerTether : InventoryItem
{
    [Header("Settings")]
    [SerializeField] LayerMask _interactionMask;
    [SerializeField] float _pullForce = 5;
    [SerializeField] float _tetherMaxLength = 20;
    [SerializeField] EventReference _tetherSFX;

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
            if (hit.collider.gameObject.TryGetComponent(out GrabbableObject grabbable) && !grabbable.CanTether) return;
            if (hit.collider.gameObject.GetComponent<FixedJoint>() == null) return;


            _connectedObject = hit.collider.gameObject;
            GameManager.Instance.Player.GetComponent<PlayerSettings>().IsAnchored = false;
            RuntimeManager.PlayOneShot(_tetherSFX);
            _tetherRb.rotation = Quaternion.LookRotation(-hit.normal);
            _tetherRb.angularVelocity = Vector3.zero;
            _tetherRb.velocity = Vector3.zero;
            _canPull = true;
            _connectedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _connectedObject.GetComponent<FixedJoint>().connectedBody = _tetherRb;

            var parent = _connectedObject.transform.GetComponentInParent<Mover>();
            if (parent != null)
                parent.enabled = false;

            if (_connectedObject.CompareTag("Tether Move"))
            {
                PullPlayer(hit.point, _connectedObject.transform);
            }
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
            if (_connectedObject != null)
            {
                _connectedObject.transform.parent = null;
                var rb = _connectedObject.GetComponent<Rigidbody>();
                if (!rb.isKinematic)
                {
                    rb.angularVelocity = Vector3.zero;
                    StartCoroutine(SetDrag(rb));
                }
                _connectedObject = null;
            }
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
            if (_connectedObject == null || (_connectedObject != null && !_connectedObject.TryGetComponent(out MoveAnchor _)))
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

        _tetherRb.AddForce(Camera.main.transform.forward * 1000);
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Player.GetComponent<PlayerSettings>().IsAnchored = false;
    }

    public override void Equip(bool isEquipped, bool forceEquip = false)
    {
        ShowHeld(true);
        base.Equip(isEquipped);
    }

    void ShowHeld(bool visible)
    {
        foreach (var renderer in _renderers) renderer.enabled = visible;
    }

    IEnumerator SetDrag(Rigidbody rb)
    {
        rb.drag = 1;
        yield return new WaitForSeconds(2);
        if (rb != null)
            rb.drag = 0;
    }

    void PullPlayer(Vector3 hitPoint, Transform tetherPoint)
    {
        var player = GameManager.Instance.Player.GetComponent<Rigidbody>();
        player.DOMove(hitPoint - _tether.transform.forward, Vector3.Distance(player.position, _tetherRb.position) / 10)
            .OnComplete(() =>
            {
                player.velocity = Vector3.zero;
                GameManager.Instance.MovePlayer(tetherPoint, offset: tetherPoint.forward, snap: false, rotate: false);
                // EventManager.Instance.BroadcastEvent("on-tether", tetherPoint);
                PlayerSettings.Instance.IsAnchored = true;
                tetherPoint.GetComponent<FixedJoint>().connectedBody = null;
            });
    }
}