using Reflex.Attributes;
using UnityEngine;

public class GoldTester : MonoBehaviour
{
    private IWalletService _wallet;
    
    [Inject]
    public void Construct(IWalletService wallet)
    {
        _wallet = wallet;
    }

    private void Start()
    {
        _wallet.AddGold(100);
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