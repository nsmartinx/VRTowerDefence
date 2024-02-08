using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaveSpawner1 : MonoBehaviour
{
    public float timeBetweenWaves = 10f;
    public float countdown;
    public Transform spawnpoint;

    public int waveIndex = 0;
    private int lastSpawnedWave = 0;

    string soldier = "Soldier 1", spy = "Spy 1", MOAB = "MOAB 1", superSoldier = "Super Soldier 1", tank = "Tank 1", minion = "Minion 1";


    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {

    }
    void Update()
    {
        if (lastSpawnedWave >= waveIndex)
            StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        waveIndex++;
        float waveDifficulty = Mathf.Pow(1.3f, waveIndex);
        while (waveDifficulty > 0)
        {
            if (waveDifficulty >= 1000)
            {
                SpawnUnit(MOAB);
                yield return new WaitForSeconds(0.5f);
                waveDifficulty -= 1000;
            }
            if (waveDifficulty >= 100)
            {
                SpawnUnit(superSoldier);
                yield return new WaitForSeconds(0.5f);
                waveDifficulty -= 100;
            }
            if (waveDifficulty >= 50)
            {
                SpawnUnit(spy);
                yield return new WaitForSeconds(0.5f);
                waveDifficulty -= 50;
            }
            if (waveDifficulty >= 10)
            {
                SpawnUnit(tank);
                yield return new WaitForSeconds(0.5f);
                waveDifficulty -= 10;
            }
            if (waveDifficulty >= 1)
            {
                SpawnUnit(soldier);
                yield return new WaitForSeconds(0.5f);
                waveDifficulty -= 1;
            }
            if (waveDifficulty >= 0f)
            {
                SpawnUnit(minion);
                yield return new WaitForSeconds(0.5f);
                waveDifficulty -= 0.1f;
            }
            Debug.Log(waveDifficulty);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(10f);
        lastSpawnedWave++;
    }
    void SpawnUnit(string unit)
    {
        PhotonNetwork.Instantiate(unit, spawnpoint.position, spawnpoint.rotation);
    }
    /*
    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        if (Random.Range(0, 2) == 0)
            PhotonNetwork.Instantiate("Soldier", spawnpoint.position, spawnpoint.rotation);
        else
            PhotonNetwork.Instantiate("MOAB", spawnpoint.position, spawnpoint.rotation);
    }
    */
}
