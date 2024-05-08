using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Vector3 spawnLocation;
    [SerializeField] private float spawnSpeedMax = 5f;
    [SerializeField] private Jump over;

    private float timer;
    private int randomIndex;
    private float nextSpawn;

    private float startTimer;

    private void Awake()
    {
        timer = 0;
        nextSpawn = Random.Range(0, spawnSpeedMax) + .5f;
    }


    void FixedUpdate()
    {
        if (startTimer < 5)
        {
            startTimer += Time.deltaTime;
        }

        if (!over.gameOver && startTimer > 5) 
        {
            if (timer >= nextSpawn)
            {
                randomIndex = Random.Range(0, obstacles.Length);
                Instantiate(obstacles[randomIndex], spawnLocation, transform.rotation);
                nextSpawn = Random.Range(0, spawnSpeedMax) + .4f;
                timer = 0;
            }

            if (Input.GetKey("f"))
            {
                timer += Time.deltaTime * 1.5f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

    }
}
