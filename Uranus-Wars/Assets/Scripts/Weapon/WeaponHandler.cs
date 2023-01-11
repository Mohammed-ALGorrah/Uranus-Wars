using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class WeaponHandler : NetworkBehaviour
{
    public GrenadeHandler grenadePrefab;
    public RocketHandler rocketPrefab;

    [Networked(OnChanged = nameof(OnFireChanged))]
    public bool isFiring { get; set; }

    public ParticleSystem fireParticleSystem;

    public Transform aimPoint;

    public LayerMask collisionlayers;

    float lastTimeFired = 0;

    TickTimer grenadeFireDelay = TickTimer.None;
    TickTimer rocketFireDelay = TickTimer.None;

    HPHandler hpHandler;

    NetworkPlayer networkPlayer;
    NetworkObject networkObject;


    private void Awake()
    {
        hpHandler = GetComponent<HPHandler>();
        networkPlayer = GetBehaviour<NetworkPlayer>();
        networkObject = GetComponent<NetworkObject>();
    }

    public override void FixedUpdateNetwork()
    {
        if (hpHandler.isDead)
        {
            return;
        }

        if (networkPlayer.canPlay)
        {
            if (GetInput(out NetworkInputData networkInputData))
            {
                if (networkInputData.isFireButtonPressed)
                {
                    Fire(networkInputData.aimForwardVector);
                }

                if (networkInputData.isGrenadeFireButtonPressed)
                {
                    FireGrenade(networkInputData.aimForwardVector);
                }

                if (networkInputData.isRocketLauncherFireButtonPressed)
                {
                    FireRocket(networkInputData.aimForwardVector);
                }
            }
        }
    }

    void Fire(Vector3 aimForwardVector)
    {
        if (Time.time - lastTimeFired < 0.15f)
        {
            return;
        }

        StartCoroutine(FireEffect());

        Runner.LagCompensation.Raycast(aimPoint.position,aimForwardVector,100.0f,Object.InputAuthority,out var hitInfo, collisionlayers,HitOptions.IgnoreInputAuthority);

        float hitDistance = 100;
        bool isHitOtherPlayer = false;

        if (hitInfo.Distance > 0)
        {
            hitDistance = hitInfo.Distance;
        }

        if (hitInfo.Hitbox != null)
        {
            if (Object.HasStateAuthority)
            {
                hitInfo.Hitbox.transform.root.GetComponent<HPHandler>().OnTakeDamage(networkPlayer.nickName.ToString(),1);
            }

            isHitOtherPlayer = true;
        }

        if (isHitOtherPlayer)
            Debug.DrawRay(aimPoint.position, aimForwardVector * hitDistance, Color.red, 1);
        else Debug.DrawRay(aimPoint.position, aimForwardVector * hitDistance, Color.green, 1);

        lastTimeFired = Time.time;
    }

    void FireGrenade(Vector3 aimForwardVector)
    {
        if (grenadeFireDelay.ExpiredOrNotRunning(Runner))
        {
            Runner.Spawn(grenadePrefab,aimPoint.position + aimForwardVector * 1.5f,Quaternion.LookRotation(aimForwardVector),Object.InputAuthority,(runner,spawnedGrenade) =>
            {
                spawnedGrenade.GetComponent<GrenadeHandler>().Throw(aimForwardVector * 15,Object.InputAuthority,networkPlayer.nickName.ToString());
            });

            grenadeFireDelay = TickTimer.CreateFromSeconds(Runner,1.0f);
        }
    }

    void FireRocket(Vector3 aimForwardVector)
    {
        if (rocketFireDelay.ExpiredOrNotRunning(Runner))
        {
            Runner.Spawn(rocketPrefab, aimPoint.position + aimForwardVector * 1.5f, Quaternion.LookRotation(aimForwardVector), Object.InputAuthority, (runner, spawnedRocket) =>
            {
                spawnedRocket.GetComponent<RocketHandler>().Fire(Object.InputAuthority,networkObject, networkPlayer.nickName.ToString());
            });

            rocketFireDelay = TickTimer.CreateFromSeconds(Runner, 2.0f);
        }
    }

    IEnumerator FireEffect()
    {
        isFiring = true;

        fireParticleSystem.Play();

        yield return new WaitForSeconds(0.09f);

        isFiring = false;
    }

    static void OnFireChanged(Changed<WeaponHandler> changed)
    {
        bool isFiringCurrent = changed.Behaviour.isFiring;

        changed.LoadOld();

        bool isFiringOld = changed.Behaviour.isFiring;

        if (isFiringCurrent && !isFiringOld)
        {
            changed.Behaviour.OnFireRemote();
        }
    }

    void OnFireRemote()
    {
        if (!Object.HasInputAuthority)
        {
            fireParticleSystem.Play();
        }
    }
}
