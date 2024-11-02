using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlayerThruster : InventoryItem
{
    [Header("Settings")]
    [SerializeField] LayerMask _interactionMask;
    [field: SerializeField] public float ThrusterRange { get; private set; } = 5;

    [Header("References")]
    [SerializeField] PlayerLocalInput _input;
    [SerializeField] Rigidbody _rb;

    Vector3 _startPos;

    EventManager _evt;

    void Start()
    {
        _evt = EventManager.Instance;
        _startPos = transform.localPosition;
        Equip(false);
    }

    void Update()
    {
        if (!IsEquipped || !_input.IsFiring) return;

        // lazy to rewrite to have generic player thruster so i'll just use events.
        // performance? idk lol dont care anymore
        // laziest piece of code ever
        _evt.BroadcastEvent("player-thrust", this, _interactionMask);
        _rb.AddForce(-Camera.main.transform.forward * 0.5f);
    }

    public override void Equip(bool visiblity)
    {
        gameObject.SetActive(visiblity);

        transform.DOLocalMove(_startPos, 2f);
        base.Equip(visiblity);
    }

    public override void Dequip()
    {
        transform.DOLocalMove(_startPos + Vector3.back, 2f);
        base.Dequip();
    }
}
