using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    
    public GameObject bulletPrefab;        //탄알 원본 프리팹
    public float spawnRateMin = 0.5f;       //새 탄알 생성하는 시간의 최솟값
    public float spawnRateMax = 3f;         //새 탄알 생성하는 시간의 최댓값

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;

        spawnRate = Random.Range(spawnRateMin,spawnRateMax);

        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
     timeAfterSpawn += Time.deltaTime;

     if(timeAfterSpawn >= spawnRate){
         timeAfterSpawn = 0f;
         GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
         bullet.transform.LookAt(target);
         spawnRate = Random.Range(spawnRateMin,spawnRateMax);
     }   
    }
}
