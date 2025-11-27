using UnityEngine;
using Reflex.Core;

public class ProjectInstaller : MonoBehaviour,IInstaller
{
    [SerializeField] private PlayerConfig _playerConfig;
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(typeof(WalletService), typeof(IWalletService));
        containerBuilder.AddSingleton(_playerConfig);
    }
}
