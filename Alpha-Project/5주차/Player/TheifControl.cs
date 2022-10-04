using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class TheifControl : NetworkBehaviour
{
    private Transform target;
    
    public float detectRange = 4f;

    private bool IsOpenSafe = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.CompareTag("BeTheif"))
        {
            //Debug.Log("Theif!");
            UpdateTarget();
            
            
        }
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
