using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerThruster : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _thrusterRange;
    [SerializeField] LayerMask _interactionMask;

    [Header("References")]
    [SerializeField] GameObject _thrusters;
    [SerializeField] PlayerLocalInput _input;
    [SerializeField] Rigidbody _rb;
    public bool IsShowing { get; private set; } = false;

    Vector3 _startPos;

    void Start()
    {
        _startPos = _thrusters.transform.localPosition;
        _thrusters.SetActive(false);
        SetThrusterVisiblity(false);
    }

    void Update()
    {
        if (!IsShowing || !_input.IsFiring) return;

        _rb.AddForce(-Camera.main.transform.forward * 0.5f);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _thrusterRange, _interactionMask))
        {
            if (hit.collider.CompareTag("Clean"))
            {
                print("Clean");
                var clean = hit.collider.GetComponent<Cleanable>();
                clean.CleanPercent -= Time.deltaTime;
            }

        }
    }

    public void SetThrusterVisiblity(bool visiblity)
    {
        IsShowing = visiblity;
        _thrusters.SetActive(visiblity);

        var targPos = visiblity ? _startPos : _startPos + Vector3.back;
        _thrusters.transform.DOLocalMove(targPos, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * _thrusterRange);
    }
}
