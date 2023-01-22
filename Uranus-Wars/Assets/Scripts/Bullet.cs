using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : NetworkBehaviour
{
	ParticleSystem onHitfx;
	ParticleSystem projectileFx;
	[HideInInspector]
	public float damage; 
	Transform parent;
    TickTimer maxLiveDurationTickTimer = TickTimer.None;

    void Start()
    {
	    parent = transform.parent;
        maxLiveDurationTickTimer = TickTimer.CreateFromSeconds(Runner, 10);
        //Check if the rocket has reached the end of its life

    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            if (maxLiveDurationTickTimer.Expired(Runner))
            {
                Runner.Despawn(GetComponent<NetworkObject>());

                return;
            }
        }
        transform.position += transform.forward * Time.deltaTime * 2;
    }
}
