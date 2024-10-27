using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _range = 5;
    [SerializeField] LayerMask _interactMask;

    [Header("References")]
    [SerializeField] PlayerLocalInput _input;
    [SerializeField] PlayerSettings _settings;

    bool _isInteracting;

    Vector3 _prevPos;
    Quaternion _prevRot;

    public void Interact()
    {
        if (_isInteracting)
        {
            _isInteracting = false;
            MoveCamera(_prevPos, _prevRot);
            _prevPos = Vector3.zero;
            _prevRot = Quaternion.identity;
            _settings.IsFrozen = false;
        }
        if (_settings.IsFrozen) return;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _range, _interactMask) && !_isInteracting)
        {
            if (hit.collider.TryGetComponent(out GameInteractable interactable))
            {
                if (interactable.TryInteract())
                    _isInteracting = true;
            }
        }
    }

    public void MoveCamera(Vector3 pos, Quaternion rot)
    {
        if (pos == Vector3.zero || rot == Quaternion.identity) return;
        _settings.IsFrozen = true;
        _prevPos = Camera.main.transform.position;
        _prevRot = Camera.main.transform.rotation;
        Camera.main.transform.SetPositionAndRotation(pos, rot);
    }
}