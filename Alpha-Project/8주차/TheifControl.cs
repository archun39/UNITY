using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking.Types;

public class TheifControl : NetworkBehaviour
{
    private Transform target;
    public GameObject Jail;

    public float detectRange = 4f;

    private bool IsTeleport = false;
    private bool IsOpenSafe = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PnTPlayerSetting>().GetJob() == "Theif" && IsLocalPlayer)
        {
            UpdateTarget();
        }
    }

    public bool GetIsTeleport()
    {
        return IsTeleport;
    }

    private void UpdateTarget()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, detectRange);
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].gameObject.CompareTag("Safe"))
                {
                    
                    target = cols[i].gameObject.transform;
                    OpenSafe();
                    if (IsOpenSafe == true)
                    {
                        cols[i].gameObject.tag = "OpenedSafe";
                    }
                }
                else target = null;

            }
        }
    }

    public void GoJail()
    {
        Debug.Log("GOJAIL");
        transform.position = Jail.transform.position;
    }

    public void Teleport(Vector3 destination)
    {
        IsTeleport = true;
        transform.position = destination;
        StartCoroutine(teleportCoolTime(10f));
    }

    IEnumerator teleportCoolTime(float cool)
    {
        
        yield return new WaitForSeconds(cool);
        IsTeleport = false;
    }
    private void OpenSafe()
    {
        Debug.Log("Press F");
        if (Input.GetButtonDown("ActionKey"))
        {
            Debug.Log("OpenSafe!");
            IsOpenSafe = true;
        }
    }

    
}


