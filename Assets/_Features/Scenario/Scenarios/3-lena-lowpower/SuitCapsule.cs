
public class SuitCapsule : GameInteractable
{
    public override bool TryInteract()
    {
        // run cutscene
        EventManager.Instance.BroadcastEvent("wearing-spacesuit", true);
        base.TryInteract();
        return false;
    }
}