using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpaceShipAttack : NetworkBehaviour
{
    SpaceshipSO spaceShipInfo;
    SpaceShipMovement movment;
    NavMeshAgent navAgent;
    Transform FirePoint;
    float nextTimeToFire;
    bool canAttackMain;
    bool canAttackSec;
    NetworkObject networkObject;

    private void Awake()
    {
        networkObject = GetComponent<NetworkObject>();
    }

    void Start()
    {
        spaceShipInfo = GetComponent<SpaceShip>().spaceShipInfo;
        navAgent = GetComponent<NavMeshAgent>();
        movment = GetComponent<SpaceShipMovement>();
        FirePoint = transform.Find("FirePoint").transform;
    }

    public override void FixedUpdateNetwork()
    {
        if (movment.secTarget != null)
        {
            if (Vector3.Distance(transform.position, movment.secTarget.position) < 6)
            {
                navAgent.speed = 0;
                canAttackSec = true;
                attack();
            }
            else
            {
                canAttackSec = false;
                navAgent.speed = spaceShipInfo.speed;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, movment.mainTarget.position) < 6)
            {
                navAgent.speed = 0;
                canAttackMain = true;
                attack();
            }
            else
            {
                canAttackMain = false;
                navAgent.speed = spaceShipInfo.speed;
            }
        }
    }


    void attack()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / spaceShipInfo.fireRate;

            Runner.Spawn(spaceShipInfo.bulletPrefab, FirePoint.position, Quaternion.LookRotation(FirePoint.forward,FirePoint.up), Object.StateAuthority, (runner, spawnedBullet) =>
            {
                spawnedBullet.GetComponent<Bullet>().damage = spaceShipInfo.power;
                spawnedBullet.GetComponent<Bullet>().Fire(networkObject);
            });

            //Bullet bullet = Instantiate(spaceShipInfo.bulletPrefab);
            //bullet.FirePoint = FirePoint;
            //bullet.transform.SetParent(transform);
            //bullet.transform.position = FirePoint.position;
            //bullet.damage = spaceShipInfo.power;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Targetable"))
        {
            if (movment.secTarget == null)
            {
                movment.secTarget = other.gameObject.transform;
            }
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Targetable"))
        {
            if (movment.secTarget == null)
            {
                movment.secTarget = coll.gameObject.transform;
            }
            if (canAttackMain)
            {
                movment.lookAtMainTarget();
            }
            else if (canAttackSec)
            {
                movment.lookAtSecoundTarget();
            } 
        }
    }

}
