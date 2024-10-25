using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _range = 5;
    [SerializeField] LayerMask _interactMask;

    [Header("References")]
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
            _settings.IsFrozen = false;
        }
        if (_settings.IsFrozen) return;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _range, _interactMask))
        {
            if (hit.collider.TryGetComponent(out GameInteractable interactable))
            {
                interactable.Interact();
                _isInteracting = true;
            }
        }
    }

    public void MoveCamera(Vector3 pos, Quaternion rot)
    {
        _settings.IsFrozen = true;
        _prevPos = pos;
        _prevRot = rot;
        Camera.main.transform.SetPositionAndRotation(pos, rot);
    }
}