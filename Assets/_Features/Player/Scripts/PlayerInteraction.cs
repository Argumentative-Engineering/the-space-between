using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _range = 5;
    [SerializeField] LayerMask _interactMask;

    [Header("References")]
    [SerializeField] PlayerLocalInput _input;
    [SerializeField] PlayerSettings _settings;
    [SerializeField] TextMeshProUGUI _tooltipText;
    [SerializeField] Image _crosshairImage;

    bool _isInteracting;
    GameInteractable _interactable;

    float _opacity;

    readonly float _fadeSpeed = 5;

    Vector3 _camPrevPos;
    Quaternion _camPrevRot;

    public void Interact()
    {
        if (_isInteracting)
        {
            _isInteracting = false;
            MoveCamera(_camPrevPos, _camPrevRot);
            _camPrevPos = Vector3.zero;
            _camPrevRot = Quaternion.identity;
            _settings.IsFrozen = false;
        }
        if (_settings.IsFrozen) return;

        if (_interactable != null && !_isInteracting)
        {
            if (_interactable.TryInteract())
                _isInteracting = true;
        }

    }

    void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _range, _interactMask))
        {
            if (hit.collider.TryGetComponent(out GameInteractable interactable))
            {
                _interactable = interactable;

                _opacity = Mathf.Clamp01(_opacity += Time.deltaTime * _fadeSpeed);
                if (_interactable.Tooltip != null)
                    _tooltipText.text = _interactable.Tooltip;
            }
        }
        else
        {
            _interactable = null;
            _opacity = Mathf.Clamp01(_opacity -= Time.deltaTime * _fadeSpeed);
        }

        _crosshairImage.color = new Color(1, 1, 1, _opacity);
        _tooltipText.color = new Color(1, 1, 1, _opacity);
    }

    public void MoveCamera(Vector3 pos, Quaternion rot)
    {
        if (pos == Vector3.zero || rot == Quaternion.identity) return;
        _settings.IsFrozen = true;
        _camPrevPos = Camera.main.transform.position;
        _camPrevRot = Camera.main.transform.rotation;
        Camera.main.transform.SetPositionAndRotation(pos, rot);
    }
}