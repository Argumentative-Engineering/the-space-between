using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMover : MonoBehaviour
{
    [SerializeField] Vector3 _velocity;
    [SerializeField] Vector3 _angularVelocity;

    private void Start()
    {
        _velocity = new Vector3(
            Random.Range(-5f, 5f),
             Random.Range(-5f, 5f),
             Random.Range(-5f, 5f)
        );

        _angularVelocity = new Vector3(
             Random.Range(-5f, 5f),
             Random.Range(-5f, 5f),
             Random.Range(-5f, 5f)
        );
    }

    void Update()
    {

        transform.Translate(_velocity * Time.deltaTime);
        transform.Rotate(_angularVelocity * Time.deltaTime);
    }
}
