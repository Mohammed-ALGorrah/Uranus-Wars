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
    public NetworkObject firedByNetworkObject;
    public NetworkObject networkObject;

    private void Awake()
    {
        networkObject = GetComponent<NetworkObject>();
    }
    void Start()
    {
	    parent = transform.parent;
    }

    public void Fire(NetworkObject firedByNetworkObject)
    {
        this.firedByNetworkObject = firedByNetworkObject;
        maxLiveDurationTickTimer = TickTimer.CreateFromSeconds(Runner, 10);
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
