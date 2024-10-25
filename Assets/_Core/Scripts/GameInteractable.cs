using UnityEngine;
using UnityEngine.Events;

public class GameInteractable : MonoBehaviour
{
    public UnityEvent OnInteract;
    public bool RunOnce;

    public virtual void Interact()
    {
        OnInteract?.Invoke();
        if (RunOnce) Destroy(this);
    }
}