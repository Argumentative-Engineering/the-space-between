using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : GameInteractable
{
    [SerializeField] float _holddistance = 1.5f;
    [SerializeField] float _holdforce = 5f;
    private Rigidbody _rb;
    private bool _isheld = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override bool TryInteract()
    {
        if (_isheld)
        {
            DropItem();
        }
        else
        {
            PickupItem();
        }

        return base.TryInteract();
    }

    private void PickupItem()
    {
        if (_rb == null) return;
        // _rb.isKinematic = true;
        _isheld = true;
        _rb.drag = 10;

    }

    private void DropItem()
    {
        if (_rb == null) return;
        _isheld = false;
        //_rb.isKinematic = false;
        _rb.drag = 0;
        _rb.AddForce(Camera.main.transform.forward * 2f, ForceMode.Impulse);
        GameManager.Instance.Player.GetComponent<PlayerInteraction>().IsInteracting = false;
    }

    void FixedUpdate()
    {
        if (_isheld && Camera.main != null)
        {
            Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * _holddistance;
            Vector3 directionToTarget = targetPosition - _rb.position;
            if (directionToTarget.magnitude > 0.5f)
            {
                Vector3 desiredVelocity = directionToTarget.normalized * _holdforce;
                _rb.velocity = desiredVelocity;
                _rb.rotation = Camera.main.transform.rotation;
                _rb.angularVelocity = Vector3.zero;
            }
        }

    }
}


