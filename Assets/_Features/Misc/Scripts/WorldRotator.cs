using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 2;
    [SerializeField] float _moonRotationSpeed = 2;
    [SerializeField] Material _skybox;

    float _rot = 0;

    void Update()
    {
        _rot += Time.deltaTime;
        _skybox.SetFloat("_Rotation", (-_rot * _rotationSpeed % 360) + 180);
        transform.rotation = Quaternion.Euler(0, _rot * _moonRotationSpeed, 0);
    }
}
