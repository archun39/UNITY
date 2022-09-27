using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class PlayerMovement : NetworkBehaviour
{
    public NetworkVariable<FixedString32Bytes> nickName = new NetworkVariable<FixedString32Bytes>();
    // Start is called before the first frame update
    private bool w;
    private bool a;
    private bool s;
    private bool d;

    public override void OnNetworkSpawn()
    {
        if (IsOwner) return;
        SendNickNameServerRpc($"Player{OwnerClientId}");
    }
    private void FixedUpdate()
    {
        if (!IsOwner) return;
        w = Input.GetKey(KeyCode.W);
        s = Input.GetKey(KeyCode.S);
        a = Input.GetKey(KeyCode.A);
        d = Input.GetKey(KeyCode.D);
        
        SendInputServerRpc(w,s,a,d);
    }

    [ServerRpc]
    public void SendInputServerRpc(bool w, bool s, bool a, bool d)
    {
        Vector3 input = Vector3.zero;

        if (w)
        {
            input += Vector3.forward;
        }
        if (s)
        {
            input += Vector3.back;
        }
        if (a)
        {
            input += Vector3.left;
        }
        if (d)
        {
            input += Vector3.right;
        }

        transform.Translate(input * 0.01f, Space.World);
    }

    [ServerRpc]
    public void SendNickNameServerRpc(string s)
    {
        nickName.Value = s;
    }
}
