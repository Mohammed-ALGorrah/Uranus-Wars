using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : NetworkBehaviour
{
    private void Start()
    {
        
    }
    public float currentHealth;

    public float maxHealth;

    public void TakeDamage(float amount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= amount;
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
