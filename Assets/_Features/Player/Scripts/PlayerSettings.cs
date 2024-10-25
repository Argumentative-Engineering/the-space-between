using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [Range(0, 10)]
    public float MouseSensitivity;
    public Vector2 LookClamp;
    public bool IsFrozen;
}