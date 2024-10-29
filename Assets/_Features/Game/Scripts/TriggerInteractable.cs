using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class TriggerInteractable : GameInteractable
{
    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        TryInteract();
        Trigger();
    }

    protected virtual void Trigger() { }
}
