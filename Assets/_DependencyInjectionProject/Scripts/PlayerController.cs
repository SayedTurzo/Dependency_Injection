using Reflex.Attributes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Drag your child Gun here in the Inspector
    public PlayerGun myGun; 
    
    [Inject] private IWalletService _wallet; 

    public void SetupPlayer(PlayerInfo info)
    {
        // Pass the specific data down to the gun
        myGun.Initialize(info);
    }
    
    private void Start()
    {
        _wallet.AddGold(Random.Range(100, 1000));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Attempting to buy a potion (50g)...");
            _wallet.SpendGold(50);
        }
    }
}