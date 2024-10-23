using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string Speaker;
    public string DialogueText;
    public AudioClip DialogueVoiceOver;
}

[CreateAssetMenu(fileName = "DialogueSequence", menuName = "Narrative/Dialogue Sequence", order = 0)]
public class DialogueSequence : ScriptableObject
{
    public bool IsContinuous = true;
    public List<DialogueLine> Lines;
}