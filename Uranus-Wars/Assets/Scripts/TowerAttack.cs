using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : NetworkBehaviour
{
	TowerSO towerInfo;
	public Transform lookX,lookY,FirePoint;
	float nextTimeToFire = 0;
	public Transform xLookPoint;
	public Transform yLookPoint;
	void Start()
    {
	    towerInfo = GetComponent<Tower>().towerInfo;
    }

    
	void attack(Transform target)
	{
		
		Transform t = yLookPoint;
		t.position = new Vector3(target.position.x, lookY.position.y, target.position.z);
		t.rotation = Quaternion.Euler(target.rotation.x, lookY.rotation.y, lookY.rotation.z);
		t.localScale = lookY.localScale;
		lookY.LookAt(t);
		
		Transform t2 = xLookPoint;
		t2.position = new Vector3(target.position.x, target.position.y, target.position.z);
		t2.rotation = Quaternion.Euler(target.rotation.x, lookX.rotation.y, lookX.rotation.z);
		t2.localScale = lookX.localScale;
		lookX.LookAt(t2);
		
		if (Time.time >= nextTimeToFire)
		{
			nextTimeToFire = Time.time + 1f / towerInfo.fireRate;
			
			//Bullet bullet = Instantiate(towerInfo.bulletPrefab);
			//bullet.FirePoint = FirePoint;
			//bullet.transform.SetParent(transform);
			//bullet.transform.position = FirePoint.position;
			//bullet.damage = towerInfo.power;
		}
		
		
	}
	
	void OnTriggerStay(Collider coll)
	{
		if (coll.CompareTag("SpaceShip"))
		{
			attack(coll.transform);
		}
	}
}
