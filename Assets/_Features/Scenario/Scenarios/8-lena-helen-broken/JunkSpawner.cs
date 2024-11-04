using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _junkList;
    [SerializeField] int _spawnCount;

    void Start()
    {
        var bounds = GetComponent<BoxCollider>().bounds;
        var min = bounds.min;
        var max = bounds.max;
        for (int i = 0; i < _spawnCount; i++)
        {
            var toSpawn = _junkList[Random.Range(0, _junkList.Length - 1)];

            var pos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));

            float[] rots = new float[3];
            for (int rot = 0; rot < 3; rot++)
                rots[rot] = Random.Range(0, 360);

            Instantiate(toSpawn, pos, Quaternion.Euler(rots[0], rots[1], rots[2]));
        }
    }
}
