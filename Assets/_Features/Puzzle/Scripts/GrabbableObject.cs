using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : GameInteractable
{
    [SerializeField] Transform _anchor;
    [SerializeField] float _holddistance = 1.5f;
    [SerializeField] float _holdforce = 5f;
    private Rigidbody _rb;
    private bool _isHeld = false;

    Vector3 _targPos, _dir;
    string _originalTooltip;

    void Start()
    {
        _originalTooltip = Tooltip;
        _rb = GetComponent<Rigidbody>();
        Tooltip = "Grab " + _originalTooltip;
    }

    public override bool TryInteract()
    {
        if (_isHeld)
        {
            DropItem();
        }
        else
        {
            PickupItem();
        }

        return false;
    }

    private void PickupItem()
    {
        if (_rb == null) return;
        Tooltip = "Throw " + _originalTooltip;

        _isHeld = true;
        _rb.drag = 20;
    }

    private void DropItem()
    {
        if (_rb == null) return;
        Tooltip = "Grab " + _originalTooltip;
        _isHeld = false;
        _rb.drag = 0;

        var dir = Camera.main.transform.forward;
        _rb.AddForce(dir * 2f, ForceMode.Impulse);
        GameManager.Instance.Player.GetComponent<PlayerInteraction>().IsInteracting = false;

        // pusbback player
        dir.y /= 2;
        GameManager.Instance.Player.GetComponent<Rigidbody>().AddForce(dir * -2f, ForceMode.Impulse);
    }

    private void Update()
    {
        if (_isHeld && Camera.main != null)
        {
            _targPos = Camera.main.transform.position + Camera.main.transform.forward * _holddistance;
            _dir = _targPos - _rb.position;

            if (Vector3.Distance(transform.position, _targPos) > 0.1f)
            {
                _rb.AddForce(_holdforce * 100 * Time.deltaTime * _dir.normalized, ForceMode.VelocityChange);
                _rb.rotation = Camera.main.transform.rotation;
                _rb.angularVelocity = Vector3.zero;
            }
        }
    }
}


