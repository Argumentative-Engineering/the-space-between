using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class GameTrigger : MonoBehaviour
{
    public UnityEvent OnTrigger;

    Collider _col;
    private void OnValidate()
    {
        _col = GetComponent<Collider>();
        _col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTrigger?.Invoke();
    }
}
