using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Drag the SAME "PlayerDeathChannel" asset here
    public VoidEventChannelSO onPlayerDeath;

    void OnEnable()
    {
        // Subscribe to the channel
        onPlayerDeath.OnEventRaised += ShowGameOver;
    }

    void OnDisable()
    {
        // Always unsubscribe!
        onPlayerDeath.OnEventRaised -= ShowGameOver;
    }

    void ShowGameOver()
    {
        Debug.Log("UI: Showing Game Over Screen!");
    }
}