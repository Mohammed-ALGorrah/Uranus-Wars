using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpaceShip : NetworkBehaviour
{
    
	public SpaceshipSO spaceShipInfo;
	HealthSystem healthSystem;
	NavMeshAgent navagent;
	NetworkObject networkObject;
	
    void Awake()
    {
		networkObject = GetComponent<NetworkObject>();
	    healthSystem = GetComponent<HealthSystem>();
	    navagent = GetComponent<NavMeshAgent>();
	    GetComponent<SphereCollider>().radius = spaceShipInfo.maxRange;
	    healthSystem.maxHealth = spaceShipInfo.maxHealth;
	    healthSystem.currentHealth = spaceShipInfo.maxHealth;
	    navagent.speed = spaceShipInfo.speed;

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
