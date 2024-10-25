using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _range = 5;
    [SerializeField] LayerMask _interactMask;

    public void Interact()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _range, _interactMask))
        {
            if (hit.collider.TryGetComponent(out GameInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}