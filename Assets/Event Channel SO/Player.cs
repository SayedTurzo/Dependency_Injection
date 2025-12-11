using UnityEngine;

public class Player : MonoBehaviour
{
    // Drag the "PlayerDeathChannel" asset here in Inspector
    public VoidEventChannelSO onPlayerDeath;

    [ContextMenu("Die")]
    public void Die()
    {
        Debug.Log("Player: I am dead.");
        
        // Broadcast the message!
        // Notice: We don't know WHO is listening. We just shout into the void.
        onPlayerDeath.RaiseEvent(); 
        
        Destroy(gameObject); // Even if player is destroyed, the signal was sent!
    }
}