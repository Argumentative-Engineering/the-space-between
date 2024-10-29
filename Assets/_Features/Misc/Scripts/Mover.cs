using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] Vector3 _velocity;
    [SerializeField] Vector3 _angularVelocity;

    void Update()
    {
        transform.Translate(_velocity * Time.deltaTime);
        transform.Rotate(_angularVelocity * Time.deltaTime);
    }
}
