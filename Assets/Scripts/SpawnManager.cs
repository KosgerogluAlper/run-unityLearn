using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] obstaclePrefab;
    private PlayerController playerController;

    private Vector3 spawnPos = new(25, 0, 0);
   
    readonly private float spawnRate = 2f;
    readonly private float startDelay = 4f;
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating(nameof(SpawnObstacle), startDelay, spawnRate);
    }

    void SpawnObstacle()
    {

        if (playerController.gameOver == false)
        {
            int randomInt=Random.Range(0,obstaclePrefab.Length);
            Instantiate(obstaclePrefab[randomInt], spawnPos, obstaclePrefab[randomInt].transform.rotation);
        }
    }



}
