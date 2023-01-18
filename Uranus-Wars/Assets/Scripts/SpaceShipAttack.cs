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
                navAgent.isStopped = true;
                //navAgent.enabled = false;
                
                attack();
            }
            else
            {
                canAttackSec = false;
                //navAgent.enabled = true;
                navAgent.isStopped = false;
                navAgent.speed = spaceShipInfo.speed;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, movment.mainTarget.position) < 6)
            {
                navAgent.speed = 0;
                canAttackMain = true;
                //navAgent.enabled = false;
                navAgent.isStopped = true;
                attack();
            }
            else
            {
                canAttackMain = false;
                //navAgent.enabled = true;
                navAgent.isStopped = false;
                navAgent.speed = spaceShipInfo.speed;
            }
        }
    }


    void attack()
    {
        //if (Time.time >= nextTimeToFire)
        //{
        //	nextTimeToFire = Time.time + 1f / spaceShipInfo.fireRate;

        //	Bullet bullet = Instantiate(spaceShipInfo.bulletPrefab);
        //	bullet.FirePoint = FirePoint;
        //	bullet.transform.SetParent(transform);
        //	bullet.transform.position = FirePoint.position;
        //	bullet.damage = spaceShipInfo.power;
        //}
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Targetable"))
        {
            if (canAttackMain)
            {
                //movment.lookAtMainTarget();
            }
            if (canAttackSec)
            {
                //movment.lookAtSecoundTarget();
            } 
        }
    }

}
