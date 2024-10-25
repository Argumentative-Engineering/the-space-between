using UnityEngine;

public class DialogueInteractable : GameInteractable
{
    [SerializeField] DialogueData _dialogueEvent;
    [SerializeField] Transform _cameraPoint;

    public override bool TryInteract()
    {
        base.TryInteract();
        if (NarrativeManager.Instance.IsRunning) return false;
        NarrativeManager.Instance.PlayDialogue(_dialogueEvent);
        GameManager.Instance.Player.GetComponent<PlayerInteraction>().MoveCamera(_cameraPoint.position, _cameraPoint.rotation);

        return true;
    }
}