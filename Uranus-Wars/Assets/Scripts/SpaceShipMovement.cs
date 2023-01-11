using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpaceShipMovement : MonoBehaviour
{
	public SpaceshipSO spaceShipInfo;
	public Transform mainTarget,secTarget;
	NavMeshAgent navAgent;
	SpaceShipAttack spaceAttack;
	
    void Start()
    {
	    navAgent = GetComponent<NavMeshAgent>();
	    spaceAttack = GetComponent<SpaceShipAttack>();
    }

    void Update()
	{
		if (secTarget != null)
		{
			navAgent.SetDestination(secTarget.position);

		}else if(mainTarget != null)
		{
			navAgent.SetDestination(mainTarget.position);
		}else
		{
			UnityEngine.Debug.Log("you win");
		}
    }
    
    
	void OnTriggerStay(Collider coll)
	{
		if (coll.CompareTag("Targetable"))
		{
			secTarget = coll.gameObject.transform;
		}
	}
}


