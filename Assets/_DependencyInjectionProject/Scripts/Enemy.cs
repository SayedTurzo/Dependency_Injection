using Reflex.Attributes;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private IEnemyManager _enemyManager;

    [Inject]
    public void Construct(IEnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
    }

    void Start()
    {
        // When the game starts, tell the manager I exist
        _enemyManager.RegisterEnemy(gameObject.name);
    }
}