using UnityEngine;

[CreateAssetMenu(fileName = "Player Movement Data", menuName = "Gameplay/Player Movement Data", order = 0)]
public class PlayerMovementSettingsData : ScriptableObject
{
    public bool UseGravity = false;
    public float MoveSpeed;
    public float Drag = 1f;
    public bool ShowHelmet = false;
}