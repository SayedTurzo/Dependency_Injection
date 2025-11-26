using Reflex.Core;
using UnityEngine;

public class LevelInstaller : MonoBehaviour,IInstaller
{
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(typeof(EnemyManager), typeof(IEnemyManager)); 
        containerBuilder.AddSingleton(typeof(DamageService), typeof(IDamageService));
    }
}
