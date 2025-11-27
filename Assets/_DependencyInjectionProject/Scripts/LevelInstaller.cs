using Reflex.Core;
using UnityEngine;

public class LevelInstaller : MonoBehaviour,IInstaller
{
    public bool botMode = false;
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(typeof(EnemyManager), typeof(IEnemyManager)); 
        containerBuilder.AddSingleton(typeof(DamageService), typeof(IDamageService));

        if (botMode)
        {
            containerBuilder.AddSingleton(typeof(BotInput), typeof(IMovementInput));
        }
        else
        {
            containerBuilder.AddSingleton(typeof(PlayerInput), typeof(IMovementInput));
        }
    }
}
