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
	NetworkObject networkObject;

    private void Awake()
    {
        networkObject  = GetComponent<NetworkObject>();
        xLookPoint = GameObject.Find("X Look Point").transform;
        yLookPoint = GameObject.Find("Y Look Point").transform;
    }

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

            Runner.Spawn(towerInfo.bulletPrefab, FirePoint.position, Quaternion.LookRotation(FirePoint.forward, FirePoint.up), Object.StateAuthority, (runner, spawnedBullet) =>
            {
                spawnedBullet.GetComponent<Bullet>().damage = towerInfo.power;
                spawnedBullet.GetComponent<Bullet>().Fire(networkObject);
            });
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
