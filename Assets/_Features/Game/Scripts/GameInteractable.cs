using UnityEngine;
using UnityEngine.Events;

public class GameInteractable : MonoBehaviour
{
    public string Tooltip;
    public UnityEvent OnInteract;
    public bool RunOnce;

    public virtual bool TryInteract()
    {
        OnInteract?.Invoke();
        if (RunOnce) Destroy(this);
        return true;
    }
}