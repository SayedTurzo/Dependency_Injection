using System;
using UnityEngine;
using Reflex.Attributes;

public class Bullet : MonoBehaviour
{
    private IDamageService _damageService;
    
    [Inject]
    public void Construct(IDamageService damageService)
    {
        _damageService = damageService;
    }

    private void Start()
    {
        int damage = _damageService.GetDamage();
        Debug.Log($"[Bullet] Hit for {damage} damage");
    }
}
