
public class SuitCapsule : GameInteractable
{
    public override bool TryInteract()
    {
        CutsceneManager.Instance.Fade(1, () =>
        {
            EventManager.Instance.BroadcastEvent(EventDefinitions.WearingSpacesuit, true);
            CutsceneManager.Instance.Fade(0, null);
        });
        base.TryInteract();
        return false;
    }
}