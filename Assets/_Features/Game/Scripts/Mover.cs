using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] Vector3 _velocity;
    [SerializeField] Vector3 _angularVelocity;

    void Start()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0, 360));
    }

    void Update()
    {
        transform.Translate(_velocity * Time.deltaTime);
        transform.Rotate(_angularVelocity * Time.deltaTime);
    }
}
