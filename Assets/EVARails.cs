using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVARails : MonoBehaviour
{
    [SerializeField] float _maxDistFromHandrail = 5;
    [SerializeField] Transform[] _evaWaypoints;
    [SerializeField] float _damping = 1;

    Rigidbody _player;
    [SerializeField] LineRenderer _tether;
    Vector3 _closest;

    void Start()
    {
        _player = GameManager.Instance.Player.transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var playerPos = _player.position;
        _closest = FindClosestSegmentPoint(playerPos);
        var dist = Vector3.Distance(playerPos, _closest);

        if (dist > _maxDistFromHandrail)
        {
            var dirToTether = (_closest - playerPos).normalized;

            var excessDist = dist - _maxDistFromHandrail;
            var damping = dirToTether * excessDist * _damping;

            _player.AddForce(damping, ForceMode.Acceleration);
        }
    }

    void Update()
    {
        if (_closest != null)
        {
            _tether.SetPosition(0, _closest);
            _tether.SetPosition(1, _player.position);
        }
    }

    private IEnumerable<(Transform, Transform)> WaypointSegments()
    {
        for (int i = 0; i < _evaWaypoints.Length - 1; i++)
        {
            yield return (_evaWaypoints[i], _evaWaypoints[i + 1]);
        }
    }

    private Vector3 ClosestPointOnSegment(Vector3 A, Vector3 B, Vector3 P)
    {
        Vector3 AB = B - A;
        float t = Mathf.Clamp(Vector3.Dot(P - A, AB) / AB.sqrMagnitude, 0, 1);
        return A + t * AB;
    }

    private Vector3 FindClosestSegmentPoint(Vector3 playerPosition)
    {
        Vector3 closestPoint = Vector3.zero;
        float minDistance = float.MaxValue;

        foreach (var (A, B) in WaypointSegments())
        {
            Vector3 point = ClosestPointOnSegment(A.position, B.position, playerPosition);
            float distance = Vector3.Distance(point, playerPosition);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }

        return closestPoint;
    }
}
