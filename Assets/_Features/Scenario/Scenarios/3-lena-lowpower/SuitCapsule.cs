
public class SuitCapsule : GameInteractable
{
    public override bool TryInteract()
    {
        // run cutscene
        EventManager.Instance.BroadcastEvent(EventDefinitions.WearingSpacesuit, true);
        base.TryInteract();
        return false;
    }
}