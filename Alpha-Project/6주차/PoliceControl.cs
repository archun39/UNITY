using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Networking;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine.Networking.Types;
using UnityEngine.UIElements;

public class PoliceControl : NetworkBehaviour
{
    
    [SerializeField] private GameObject _target;
    public float detectRange = 4f;

    public float catchingTime = 5f;

    

    // Update is called once per frame
    void Update()
    {
        if (this.CompareTag("BePolice") && IsLocalPlayer)
        {
            UpdateTarget();
        }
    }
    private void UpdateTarget()
    {
        if (IsOwner)
        {
            //Detect Theif with Collider
            Collider[] cols = Physics.OverlapSphere(transform.position, detectRange);
            if (cols.Length > 0)
            {
                foreach (var t in cols)
                {
                    if (t.CompareTag("BeTheif"))
                    {
                        _target = t.gameObject;
                        if (_target == IsLocalPlayer)
                        {
                            if (Input.GetButtonDown("ActionKey"))
                            {
                                //Give Theif information to "Gamemanager"
                                ulong targetclientId = _target.GetComponent<NetworkObject>().OwnerClientId;
                                ulong playerobjectid = _target.GetComponent<NetworkObject>().NetworkObjectId;
                                Catch(targetclientId,playerobjectid);
                            }
                        }
                    }
                    else _target = null;
                }
            }
        }
    }
    private void Catch(ulong targetId,ulong playerobjectid)
    {
        Debug.Log("Catch");
        PnTGamemanager.gm.GetComponent<PnTGamemanager>().CheckCatchServerRpc(targetId,playerobjectid);
    }
}
