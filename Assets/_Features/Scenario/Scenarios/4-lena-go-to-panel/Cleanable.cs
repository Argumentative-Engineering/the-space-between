using UnityEngine;
using UnityEngine.Events;

public class Cleanable : MonoBehaviour
{
    [SerializeField] UnityEvent _onCleaned;
    public float CleanPercent = 5;
    [SerializeField] float _cleanThresholdPercent = 40;

    float _startPercent;
    Material _mat;

    private void Start()
    {
        _mat = GetComponent<Renderer>().material;
        _startPercent = CleanPercent;
    }

    private void Update()
    {
        if (CleanPercent <= (_startPercent * (_cleanThresholdPercent / 100)))
        {
            _onCleaned?.Invoke();
            Destroy(gameObject);
        }

        _mat.SetFloat("_CircleRadius", Mathf.Lerp(1, 0, CleanPercent / _startPercent));
    }
}