using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkInGameMessages : NetworkBehaviour
{
    InGameMessagesUIHander inGameMessagesUIHander;

    public void SendInGameRPCMessage(string userNickName,string message)
    {
        RPC_InGameMessages($"<b>{userNickName}</b> : {message}");
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_InGameMessages(string message,RpcInfo info = default)
    {
        if (inGameMessagesUIHander == null)
        {
            inGameMessagesUIHander = NetworkPlayer.Local.localCameraHandler.GetComponentInChildren<InGameMessagesUIHander>();
        }

        if (inGameMessagesUIHander != null)
        {
            inGameMessagesUIHander.OnGameMessageReceived(message);
        }
    }
}
