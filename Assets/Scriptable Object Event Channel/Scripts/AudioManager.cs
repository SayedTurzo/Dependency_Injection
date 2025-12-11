using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameEvent onPlayerHeal;

    private void OnEnable()
    {
        onPlayerHeal.AddListener(PlayHealSound);
    }
    
    private void OnDisable()
    {
        onPlayerHeal.RemoveListener(PlayHealSound);
    }
    
    private void PlayHealSound()
    {
        Debug.Log("Audio: Healing Sound Played!");
    }
}
