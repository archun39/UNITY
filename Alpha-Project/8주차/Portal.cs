using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using UnityEditor.Rendering;

public class Portal : NetworkBehaviour
{
    [SerializeField] private GameObject otherPortal;


    private Vector3 otherPortalPos;
    private void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.GetComponent<PnTPlayerSetting>().GetJob() == "Theif")
        {
            if (other.GetComponent<TheifControl>().GetIsTeleport() == false)
            {
                Vector3 destination = otherPortal.transform.position;
                ulong targetClientId = other.GetComponent<NetworkObject>().OwnerClientId;
                ulong playerObjectId = other.GetComponent<NetworkObject>().NetworkObjectId;
                PnTGamemanager.gm.PortalServerRpc(destination, targetClientId, playerObjectId);

            }
            else Debug.Log(other.GetComponent<TheifControl>().GetIsTeleport());
        }
    }
}
