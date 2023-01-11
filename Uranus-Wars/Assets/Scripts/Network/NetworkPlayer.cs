using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public TMP_Text playerNickNameTMP;
    public static NetworkPlayer Local { get; set; }

    public Transform playerModel;

    [Networked(OnChanged = nameof(OnNickNameChanged))]

    public NetworkString<_16> nickName { get; set; }

    bool isPublicJoinMessageSent = false;

    public LocalCameraHandler localCameraHandler;
    public GameObject localUI;

    NetworkInGameMessages networkInGameMessages;

    public NetworkBool canPlay = false;

    public NetworkRunner networkRunner;
    private void Awake()
    {
        networkInGameMessages = GetComponent<NetworkInGameMessages>();
        networkRunner = GameObject.Find("Network Runner").GetComponent<NetworkRunner>();
    }

    public override void FixedUpdateNetwork()
    {
        if (networkRunner == null)
        {
            networkRunner = GameObject.Find("Network Runner").GetComponent<NetworkRunner>();
        }

        if (networkRunner != null && !canPlay)
        {
            if(networkRunner.ActivePlayers.Count() > 1)
            {
                canPlay = true;
            }
        }
    }
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;

            Utils.SetRenderLayerInChildren(playerModel, LayerMask.NameToLayer("LocalPlayerModel"));

            Camera.main.gameObject.SetActive(false);

            RPC_SetNickName(PlayerPrefs.GetString("PlayerNickName"));

            Debug.Log("Spawned local player");
        }
        else
        {
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;

            AudioListener localAudioListener = GetComponentInChildren<AudioListener>();
            localAudioListener.enabled = false;

            localUI.SetActive(false);

            Debug.Log("Spawned remote player");
        }

        Runner.SetPlayerObject(Object.InputAuthority, Object);
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (Object.HasStateAuthority)
        {
            if (Runner.TryGetPlayerObject(player, out NetworkObject playerLeftNetworkObject))
            {
                if (playerLeftNetworkObject == Object)
                {
                    Local.GetComponent<NetworkInGameMessages>().SendInGameRPCMessage(playerLeftNetworkObject.GetComponent<NetworkPlayer>().nickName.ToString(), "left");
                }
            }
        }

        if (player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
    }

    static void OnNickNameChanged(Changed<NetworkPlayer> changed)
    {
        changed.Behaviour.OnNickNameChanged();
    }

    void OnNickNameChanged()
    {
        playerNickNameTMP.text = nickName.ToString();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        this.nickName = nickName;

        if (!isPublicJoinMessageSent)
        {
            networkInGameMessages.SendInGameRPCMessage(nickName, "Joined");

            isPublicJoinMessageSent = true;
        }
    }
}
