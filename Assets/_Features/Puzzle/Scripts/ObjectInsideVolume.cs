using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectInsideVolume : MonoBehaviour
{
    [SerializeField] GameObject ObjectToCount;
    public List<GameObject> ObjectsInside = new();
    public UnityEvent OnEmpty;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ObjectToCount && ObjectsInside.Contains(other.gameObject))
        {
            ObjectsInside.Remove(other.gameObject);
        }
    }

    void Update()
    {
        if (ObjectsInside.Count == 0)
        {
            OnEmpty?.Invoke();
            Destroy(this);
        }
    }

}
