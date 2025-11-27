using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private PlayerInfo _playerInfo;
    
    public void Initialize(PlayerInfo info)
    {
        _playerInfo = info;
        Debug.Log($"[Gun] Initialized with {_playerInfo.MaxHealth} HP.");
    }
}
