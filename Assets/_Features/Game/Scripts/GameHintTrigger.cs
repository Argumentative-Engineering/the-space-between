using UnityEngine;

public class GameHintTrigger : TriggerInteractable
{
    public string Hint;
    protected override void Trigger()
    {
        GameHints.Instance.ShowHint(Hint);
    }
}