using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : NetworkBehaviour
{


    public float currentHealth;
    public float maxHealth;

    public event Action<HealthSystem> OnTakeDamage;
    public event Action<HealthSystem> OnDead;

    public void TakeDamage(float amount)
    {

        if (IsDead())
        {
            return;
        }
        if (currentHealth > 0)
        {
            currentHealth -= amount;

            this.OnTakeDamage?.Invoke(this);

            if (currentHealth <= 0)
            {
                this.OnDead?.Invoke(this);
            }
        }

    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
