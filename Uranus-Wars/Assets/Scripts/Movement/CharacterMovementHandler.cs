using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{

    bool isRespawnRequested = false;

    NetworkCharacterControllerPrototypeCustome networkCharacterControllerPrototypeCustome;
    HPHandler hpHandler;
    NetworkInGameMessages networkInGameMessages;
    NetworkPlayer networkPlayer;
    void Awake()
    {
        networkCharacterControllerPrototypeCustome = GetComponent<NetworkCharacterControllerPrototypeCustome>();
        hpHandler = GetComponent<HPHandler>();
        networkInGameMessages = GetComponent<NetworkInGameMessages>();
        networkPlayer = GetComponent<NetworkPlayer>();
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            if (isRespawnRequested)
            {
                Respawn();
                return;
            }

            if (hpHandler.isDead)
            {
                return;
            }
        }

        if (networkPlayer.canPlay)
        {
            if (GetInput(out NetworkInputData networkInputData))
            {
                transform.forward = networkInputData.aimForwardVector;

                Quaternion rotation = transform.rotation;
                rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
                transform.rotation = rotation;

                Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
                moveDirection.Normalize();
                networkCharacterControllerPrototypeCustome.Move(moveDirection);

                if (networkInputData.isJumpPressed)
                {
                    networkCharacterControllerPrototypeCustome.Jump();
                }
                CheckFailRespawn();

            }
        }
    }
    
    void CheckFailRespawn()
    {
        if (transform.position.y < -12)
        {
            if (Object.HasStateAuthority)
            {
                networkInGameMessages.SendInGameRPCMessage(networkPlayer.nickName.ToString(),"fell out the world");
                Respawn();
            }
        }
    }

    public void RequestRespawn()
    {
        isRespawnRequested = true;
    }

    void Respawn()
    {
        networkCharacterControllerPrototypeCustome.TeleportToPosition(Utils.GetRandomSpawnPoint());

        hpHandler.OnRespawned();

        isRespawnRequested = false;
    }

    public void SetCharacterControllerEnabled(bool isEnabled)
    {
        networkCharacterControllerPrototypeCustome.Controller.enabled = isEnabled;
    }
}
