using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : IEnemyManager
{
    private List<string> _enemies = new List<string>();
    
    public void RegisterEnemy(string name)
    {
        _enemies.Add(name);
        Debug.Log($"[EnemyManager] Registered {name}. Total Enemies: {_enemies.Count}");    
    }
}
