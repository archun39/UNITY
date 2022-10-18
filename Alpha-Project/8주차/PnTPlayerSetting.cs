using System.Collections;
using System.Collections.Generic;
using Rito.CharacterControl;
using UnityEngine;
using Unity.Netcode;

public class PnTPlayerSetting : NetworkBehaviour
{
    private string playerJob;

    private float playerSpeed = 10f;

    private float playerRunning = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<PhysicsBasedMovement>().MOption.speed);
        Debug.Log(playerSpeed);
        GetComponent<PhysicsBasedMovement>().MOption.speed = playerSpeed;
        Debug.Log(GetComponent<PhysicsBasedMovement>().MOption.speed);
        GetComponent<PhysicsBasedMovement>().MOption.runningCoef = playerRunning;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerJob == "Police")
        {
            
        }

        if (playerJob == "Theif")
        {
            
        }
    }

    public void SetJob(string job)
    {
        playerJob = job;
    }

    public string GetJob()
    {
        return playerJob;
    }
}
