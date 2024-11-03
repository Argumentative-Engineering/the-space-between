using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitDistance : MonoBehaviour
{
    [SerializeField] Transform _targetToLimit;
    [SerializeField] float _limit;

    void Start()
    {

    }

    void Update()
    {
        var dist = Vector3.Distance(transform.position, _targetToLimit.position);

        if (dist >= _limit)
        {
            if (_targetToLimit.TryGetComponent(out Rigidbody rb)) rb.velocity = Vector3.zero;
            _targetToLimit.position = transform.position + (_targetToLimit.position - transform.position).normalized * _limit;
        }
    }
}
