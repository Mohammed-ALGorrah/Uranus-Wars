using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public TowerSO towerInfo;
	HealthSystem healthSystem;
	
	
	void Awake()
    {
	    healthSystem = GetComponent<HealthSystem>();
	    healthSystem.maxHealth = towerInfo.maxHealth;
	    healthSystem.currentHealth = towerInfo.maxHealth;
	    GetComponent<SphereCollider>().radius = towerInfo.maxRange;
    }
	
	private void OnEnable()
	{
		healthSystem.OnDead += Dead;
	}
	
	private void OnDisable()
	{
		healthSystem.OnDead -= Dead;
	}

    void Update()
    {
        
    }
    
	void Dead(HealthSystem obj)
	{
		Destroy(gameObject);
		transform.parent.GetComponent<Platform>().Tower = null;
	}
    
	void OnTriggerEnter(Collider coll)
	{
		if (coll.GetComponent<Bullet>() != null)
		{
			healthSystem.TakeDamage(coll.GetComponent<Bullet>().damage);
		}
	}
}
