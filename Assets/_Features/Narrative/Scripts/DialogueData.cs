using System.Collections.Generic;
using EditorAttributes;
using FMODUnity;
using UnityEngine;

[System.Serializable]
public class Subtitle
{
    public string Key;
    public string Speaker;
    [TextArea]
    public string Text;
}

[CreateAssetMenu(fileName = "Dialogue Data", menuName = "Narrative/Dialogue Data", order = 0)]
public class DialogueData : ScriptableObject
{
    public EventReference DialogueEvent;

    [DataTable(showLabels: true)]
    public Subtitle[] Subtitles;
}