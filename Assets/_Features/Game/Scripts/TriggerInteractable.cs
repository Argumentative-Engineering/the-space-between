using UnityEngine;


public class TriggerInteractable : GameInteractable
{
    private void OnValidate()
    {
        if (GetComponent<Collider>() != null)
            GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Trigger();
        TryInteract();
    }

    protected virtual void Trigger() { }
}
