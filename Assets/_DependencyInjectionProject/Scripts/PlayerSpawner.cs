using System;
using Reflex.Attributes;
using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    
    // We inject the container so we can pass Global Services to the player
    [Inject] private Container _container;


    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        // A. Create the Object
        GameObject newPlayer = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        // B. Inject Global Services (Wallet, Audio, etc.)
        // This makes sure any [Inject] tags inside the player still work for global stuff
        GameObjectInjector.InjectRecursive(newPlayer, _container);

        // C. Pass Local Data (HP, Stats)
        var controller = newPlayer.GetComponent<PlayerController>();
        controller.SetupPlayer(newPlayer.GetComponent<PlayerInfo>());
    }
}