using Reflex.Attributes;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private string playerName;
    [SerializeField] private string playerMail;
    [SerializeField] private float movementSpeed;
    public int MaxHealth { get; private set; }
    [SerializeField] private float damage;
    
    private PlayerConfig _playerConfig;
    
    [Inject]
    public void Construct(PlayerConfig playerConfig)
    {
        _playerConfig = playerConfig;
        InitPlayer();
    }

    void InitPlayer()
    {
        playerName = _playerConfig.playerName;
        playerMail = _playerConfig.playerMail;
        movementSpeed = _playerConfig.movementSpeed;
        MaxHealth = _playerConfig.maxHealth;
        damage = _playerConfig.damage;
        
        Debug.Log($"[PlayerInfo] Name: {playerName}, Mail: {playerMail}, Speed: {movementSpeed}, MaxHealth: {MaxHealth}, Damage: {damage}");
    }
    
}
