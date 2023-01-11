using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpaceShipAttack : MonoBehaviour
{
	SpaceshipSO spaceShipInfo;
	SpaceShipMovement movment;
	NavMeshAgent navAgent;
	Transform FirePoint;
	float nextTimeToFire;
	
    void Start()
	{
		spaceShipInfo = GetComponent<SpaceShip>().spaceShipInfo;
		navAgent = GetComponent<NavMeshAgent>();
		movment = GetComponent<SpaceShipMovement>();
		FirePoint = transform.Find("FirePoint").transform;
    }

    void Update()
	{
		if (movment.secTarget != null)
		{
		    if (Vector3.Distance(transform.position,movment.secTarget.position) < 6)
		    {
		    	navAgent.speed = 0;
		    	transform.LookAt(movment.secTarget);
		    	attack();
		    }else
		    {
		    	
			    navAgent.speed = spaceShipInfo.speed;
		    }
		}else
		{
			if (Vector3.Distance(transform.position,movment.mainTarget.position) < 6)
			{
				navAgent.speed = 0;
				transform.LookAt(movment.mainTarget);
				attack();
			}else
			{
				navAgent.speed = spaceShipInfo.speed;
			}
		}
    }
    
    
	void attack()
	{
		if (Time.time >= nextTimeToFire)
		{
			nextTimeToFire = Time.time + 1f / spaceShipInfo.fireRate;
			
			Bullet bullet = Instantiate(spaceShipInfo.bulletPrefab);
			bullet.FirePoint = FirePoint;
			bullet.transform.SetParent(transform);
			bullet.transform.position = FirePoint.position;
			bullet.damage = spaceShipInfo.power;
		}
	}
	
}
