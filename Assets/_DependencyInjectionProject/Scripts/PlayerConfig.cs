using UnityEngine;

[CreateAssetMenu(fileName = "Player Configuration", menuName = "Game Configuration/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("PLayer Stats")]
    public string playerName;
    public string playerMail;
    public float movementSpeed;
    public int maxHealth;
    public float damage;
}
