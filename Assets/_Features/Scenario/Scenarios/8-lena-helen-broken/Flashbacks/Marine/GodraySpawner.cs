using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodraySpawner : MonoBehaviour
{
    [SerializeField] GameObject _godrayPrefab;
    [SerializeField] float _cooldown = 2;

    float _timer;

    Bounds _bounds;
    void Start()
    {
        _bounds = GetComponent<BoxCollider>().bounds;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _cooldown)
        {
            SpawnGodray();
            _timer = 0;
        }
    }

    void SpawnGodray()
    {
        Vector3 pos = new(
                    Random.Range(_bounds.min.x, _bounds.max.x),
                    transform.position.y,
                    Random.Range(_bounds.min.z, _bounds.max.z)
                );

        Destroy(Instantiate(_godrayPrefab, pos, _godrayPrefab.transform.rotation), 5);
    }
}
