using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.PackageManager;

public class GateController : MonoBehaviour
{

    public Wave[] waves;
    public Transform[] _gate;
    public Wave currrentWave;
    public int currentWaveNumber;
    public bool canSpawn = true;
    public float nextSpawnTime;

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public int spawnNumber;
        public float spawnInterval;
        public GameObject[] typeOfVariant;
    }

    private void Update()
    {
        CalculateWave();
    }

    public virtual void CalculateWave()
    {
        currrentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length)
        {
            currentWaveNumber++;
            canSpawn = true;
        }
        if (currentWaveNumber == 3 && canSpawn == false && totalEnemies.Length == 0)
        {
            //Postevent spawn done
        }
    }
    public virtual void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currrentWave.typeOfVariant[Random.Range(0, currrentWave.typeOfVariant.Length)];
            Transform randomPoint = _gate[Random.Range(0, _gate.Length)];
            Instantiate(randomEnemy, randomPoint.transform.position, transform.rotation);
            currrentWave.spawnNumber--;
            nextSpawnTime = Time.time + currrentWave.spawnInterval;
            if (currrentWave.spawnNumber == 0)
            {
                canSpawn = false;
            }
        }
    }
}


