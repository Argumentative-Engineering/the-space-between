using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    [SerializeField] float _zoomoutrate = 2f;
    [SerializeField] float _maxzoomoutvalue = 100f; 
    private Camera _cam;

    void Start()
    {
        _cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (_cam.orthographic)
        {
            if (_cam.orthographicSize < _maxzoomoutvalue)
            {
                _cam.orthographicSize += _zoomoutrate * Time.deltaTime;
            }
        }
        else
        {
            if (_cam.fieldOfView < _maxzoomoutvalue)
            {
                _cam.fieldOfView += _zoomoutrate * Time.deltaTime;
            }
        }
    }
}
