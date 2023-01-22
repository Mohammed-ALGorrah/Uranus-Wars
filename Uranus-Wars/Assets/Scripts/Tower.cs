using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : NetworkBehaviour
{
	public TowerSO towerInfo;
	HealthSystem healthSystem;
	NetworkObject networkObject;
	
	
	void Awake()
    {
        networkObject = GetComponent<NetworkObject>();
        healthSystem = GetComponent<HealthSystem>();
	    healthSystem.maxHealth = towerInfo.maxHealth;
	    healthSystem.currentHealth = towerInfo.maxHealth;
	    GetComponent<SphereCollider>().radius = towerInfo.maxRange;
    }

    public override void FixedUpdateNetwork()
    {
        if (healthSystem.IsDead())
        {
            Dead();
        }
    }

    void Dead()
	{
		Runner.Despawn(GetComponent<NetworkObject>());
	}
    
	void OnTriggerEnter(Collider coll)
	{
		if (coll.GetComponent<Bullet>() != null)
		{
            if (coll.GetComponent<Bullet>().firedByNetworkObject != networkObject)
            {
                healthSystem.TakeDamage(coll.GetComponent<Bullet>().damage);
                Runner.Despawn(coll.GetComponent<Bullet>().networkObject);
            }
        }
	}
}
