public interface IWalletService
{
    int CurrentGold { get; }
    void AddGold (int amount);
    bool SpendGold (int amount);
}
