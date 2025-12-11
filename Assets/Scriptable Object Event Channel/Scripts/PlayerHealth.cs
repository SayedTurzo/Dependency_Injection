using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameEvent onPlayerHurt;
    public GameEvent onPlayerHeal;

    public void TakeDamage()
    {
        Debug.Log("Player: ওহ! ব্যথা পেয়েছি!");
        onPlayerHurt.Raise();
    }

    public void Heal(int amount)
    {
        Debug.Log("Player healed by " + amount + " points.");
        onPlayerHeal.Raise();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(100);
        }
    }
}