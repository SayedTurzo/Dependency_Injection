using UnityEngine;
using Reflex.Core;

public class ProjectInstaller : MonoBehaviour,IInstaller
{
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(typeof(WalletService), typeof(IWalletService));
    }
}
