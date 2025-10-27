using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/Player")]

public class PlayerDataSO : ScriptableObject
{
    public float velocity = 6f;
    public float reboundForce = 10f;
    public float lengthRayCast = 1f;
    public float jumpBoostTime = 5f;
    public float jumpBoostForce = 2f;
    public int maxHealth = 3;
    public int maxJumpForce = 10;
}
