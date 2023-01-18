using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpaceShip : MonoBehaviour
{
    
	public SpaceshipSO spaceShipInfo;
	HealthSystem healthSystem;
	NavMeshAgent navagent;
	
    void Awake()
    {
	    healthSystem = GetComponent<HealthSystem>();
	    navagent = GetComponent<NavMeshAgent>();
	    GetComponent<SphereCollider>().radius = spaceShipInfo.maxRange;
	    healthSystem.maxHealth = spaceShipInfo.maxHealth;
	    healthSystem.currentHealth = spaceShipInfo.maxHealth;
	    navagent.speed = spaceShipInfo.speed;

    }
	void OnEnable()
	{
		healthSystem.OnDead += Dead;
	}
	
	void OnDisable()
	{
		healthSystem.OnDead -= Dead;
	}
    
	void Dead(HealthSystem obj)
	{
		Destroy(gameObject);
	}
    
	void OnTriggerEnter(Collider coll)
	{
		if (coll.GetComponent<Bullet>() != null)
		{
			healthSystem.TakeDamage(coll.GetComponent<Bullet>().damage);
		}
	}
}
