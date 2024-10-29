using UnityEngine;
using UnityEngine.Events;

public class Cleanable : MonoBehaviour
{
    [SerializeField] UnityEvent _onCleaned;
    public float CleanPercent = 5;
    [SerializeField] float _cleanThresholdPercent = 40;
    Vector3 _startScale;

    float _startPercent;

    private void Start()
    {
        _startPercent = CleanPercent;
        _startScale = transform.localScale;
    }

    private void Update()
    {
        if (CleanPercent <= (_startPercent * (_cleanThresholdPercent / 100)))
        {
            _onCleaned?.Invoke();
            Destroy(gameObject);
        }
        transform.localScale = _startScale * (CleanPercent / 5);
    }
}