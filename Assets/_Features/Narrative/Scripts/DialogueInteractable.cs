using UnityEngine;

public class DialogueInteractable : GameInteractable
{
    [SerializeField] DialogueData _dialogueEvent;
    [SerializeField] Transform _cameraPoint;

    public override void Interact()
    {
        base.Interact();
        NarrativeManager.Instance.PlayDialogue(_dialogueEvent);
        GameManager.Instance.Player.GetComponent<PlayerInteraction>().MoveCamera(_cameraPoint.position, _cameraPoint.rotation);
    }
}