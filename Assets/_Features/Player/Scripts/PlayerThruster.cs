using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayerThruster : InventoryItem
{
    [Header("Settings")]
    [SerializeField] LayerMask _interactionMask;
    [field: SerializeField] public float ThrusterRange { get; private set; } = 5;

    public float ThrusterFuel = 100f;

    [Header("References")]
    [SerializeField] PlayerLocalInput _input;
    [SerializeField] Rigidbody _rb;
    [SerializeField] ParticleSystem _thrusterParticles;

    EventManager _evt;

    void Start()
    {
        _evt = EventManager.Instance;
    }

    void Update()
    {
        if (!IsEquipped || !_input.IsFiring || ThrusterFuel <= 0)
        {
            _thrusterParticles.Stop();
            return;
        }

        if (!_thrusterParticles.isPlaying) _thrusterParticles.Play();

        ThrusterFuel -= Time.deltaTime * 0.5f;

        // lazy to rewrite to have generic player thruster so i'll just use events.
        // performance? idk lol dont care anymore
        // laziest piece of code ever
        _evt.BroadcastEvent(EventDefinitions.PlayerThrust, this, _interactionMask);
        _rb.AddForce(-Camera.main.transform.forward * 0.5f, ForceMode.Acceleration);
    }
}
