using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[OrderBefore(typeof(NetworkTransform))]

public class SpaceShipMovement : NetworkBehaviour
{
    public SpaceshipSO spaceShipInfo;
    public Transform mainTarget, secTarget;
    NavMeshAgent navAgent;
    NetworkObject networkObject;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        networkObject = GetComponent<NetworkObject>();
    }

    public override void FixedUpdateNetwork()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Runner.Despawn(networkObject);
        }
        if (secTarget != null && navAgent.enabled)
        {
            navAgent.SetDestination(secTarget.position);
        }
        else if (mainTarget != null && navAgent.enabled)
        {
            navAgent.SetDestination(mainTarget.position);
        }
    }

    public void lookAtMainTarget()
    {
        //Debug.Log("lookAtMainTarget");
        transform.LookAt(mainTarget);
    }

    public void lookAtSecoundTarget()
    {
        //Debug.Log("lookAtSecoundTarget");
        transform.LookAt(secTarget);
    }
}


