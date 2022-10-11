using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking.Transport;
using Unity.VisualScripting;
using UnityEditor.PackageManager;

public class PnTGamemanager : NetworkBehaviour
{
    public static PnTGamemanager gm;
    [SerializeField] private GameObject jail;

    private void Awake()
    {
        Debug.Log("Success");
        gm = this;
        
    }
    
    [ServerRpc]
    public void CheckCatchServerRpc(ulong targetId,ulong playerobjectid)
    {
        if (IsServer)
        {
            Debug.Log(targetId);
            //Only work in targetClient
            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[]{targetId}
                }
            };
            GoJailClientRpc(playerobjectid,clientRpcParams);
        }
    }

    [ClientRpc]
    void GoJailClientRpc(ulong playerobjectId,ClientRpcParams rpcParams)
    {
        if (IsClient)
        {
            //Can get Target Object
            GetNetworkObject(playerobjectId).GetComponent<TheifControl>().GoJail();  
        }

    }
}
