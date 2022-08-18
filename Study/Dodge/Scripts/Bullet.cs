using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody bulletRigidbody;

    // Start is called before the first frame update
    void Start()
    {
     bulletRigidbody = GetComponent<Rigidbody>();
     bulletRigidbody.velocity = transform.forward*speed;

     Destroy(gameObject,3f);   
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            PlayerController playerController = other.GetComponent<PlayerController>();
            
            //컨트롤러 가져오는것 성공
            if(playerController!=null)
            {
                playerController.Die();
            }

        }
        
    }

    
}
