using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class PoliceControl : NetworkBehaviour
{
    public GameObject Jail;
    private GameObject _target;

    private bool _isCatch;
    public float detectRange = 5f;


    
    // Update is called once per frame
    private void Start()
    {
        
        
    }

    void Update()
    {
        if (this.CompareTag("BePolice"))
        {
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
                if (cols[i].CompareTag("BeTheif"))
                {
                    _target = cols[i].gameObject;
                    Catch(_target);

                }
                else _target = null;
            }
        }
        
    }
    
    private void Catch(GameObject theif)
    {
        if (!_isCatch)
        {
            if (Input.GetButtonDown("ActionKey"))
            {
                Debug.Log("Got You!");

                theif.transform.position = new Vector3(Jail.transform.position.x,Jail.transform.position.y + 4,Jail.transform.position.z);
                _isCatch = true;
                Debug.Log(theif.transform.position);
            }
        }
    }
}
