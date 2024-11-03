using UnityEngine;
using UnityEngine.Events;

public class GameInteractable : MonoBehaviour
{
    public string Tooltip;
    public UnityEvent OnInteract;
    public bool RunOnce;

    [SerializeField] Transform _cameraPoint;

    private void Start() { }

    public virtual bool TryInteract()
    {
        var res = false;
        if (_cameraPoint != null)
        {
            GameManager.Instance.Player.GetComponent<PlayerInteraction>().MoveCamera(_cameraPoint.position, _cameraPoint.rotation);
            res = true;
        }

        OnInteract?.Invoke();
        if (RunOnce) Destroy(this);
        return res;
    }
}