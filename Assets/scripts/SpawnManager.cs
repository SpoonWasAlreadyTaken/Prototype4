using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerupPrefab;
    private float spawnRange = 9;

    [SerializeField] GameObject boss;


    void Start()
    {
        SpawnEnemyWave(waveNumber);
        int index = Random.Range(0, powerupPrefab.Length);

        Instantiate(powerupPrefab[index], GenerateSpawnPosition(), powerupPrefab[index].transform.rotation);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int eIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[eIndex], GenerateSpawnPosition(), enemyPrefab[eIndex].transform.rotation);
        }
    }

    public int enemyCount;
    public int waveNumber = 1;
    public int wavesUntilBoss = 1;
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            wavesUntilBoss++;
            SpawnEnemyWave(waveNumber);
            int index = Random.Range(0, powerupPrefab.Length);
            Instantiate(powerupPrefab[index], GenerateSpawnPosition(), powerupPrefab[index].transform.rotation);
        }

        if (wavesUntilBoss == 5)
        {
            Instantiate(boss, transform.position, transform.rotation);
            wavesUntilBoss = 0;
        }
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

}