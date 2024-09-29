using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game Data/Player Config")]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement Settings")]
    public float movementSpeed;
    public float movementSmoothing;
    public float turnSpeed;
    public float turnSmoothing;
    public float gravity;

    [Header("Combat Settings")]
    public float delayBetweenAttacks;
    public float comboCooldown;
    
}
