using UnityEngine;

public class WalletService : IWalletService
{
    public int CurrentGold { get; private set; }

    public void AddGold(int amount)
    {
        CurrentGold += amount;
        Debug.Log($"[Wallet] Added {amount}. Total: {CurrentGold}");
    }

    public bool SpendGold(int amount)
    {
        if (CurrentGold >= amount)
        {
            CurrentGold -= amount;
            Debug.Log($"[Wallet] Spent {amount}. Remaining: {CurrentGold}");
            return true;
        }
        
        Debug.LogWarning("[Wallet] Not enough gold!");
        return false;
    }
}