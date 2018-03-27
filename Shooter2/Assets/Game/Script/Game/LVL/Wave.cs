using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

    public float timeStart;

    public GameObject[] spawnLocation;

    [Header("Waves")]
    public Waves[] waveClass;

    private int currentWave,totalWaves;
    private bool startWaves = true;
    private float[] spawnDuration;
    private int[] totalEnemys;
    private float[] spawnDurationReset;
    

    void Start ()
    {
        for (int i = 0; i < waveClass.Length; i++)
        {
            waveClass[i].objectPoolScript = new ObjectPool_Script[waveClass[i].enemys.Length];
            for (int o = 0; o < waveClass[i].enemys.Length; o++)
            {
                waveClass[i].objectPoolScript[o] = (ObjectPool_Script)waveClass[i].enemys[o].GetComponent(typeof(ObjectPool_Script));
            }
        }

        totalWaves = waveClass.Length;
        spawnDuration = new float[waveClass.Length];
        spawnDurationReset = new float[waveClass.Length];
        totalEnemys = new int[waveClass.Length];
        for (int i = 0; i < waveClass.Length; i++)
        {
            for (int o = 0; o < waveClass[i].spawnEnemys.Length; o++)
            {
                totalEnemys[i] += waveClass[i].spawnEnemys[o];
            }
            spawnDuration[i] = waveClass[i].waveSpawnDuration / totalEnemys[i];
            spawnDurationReset[i] = spawnDuration[i];
        }
	}
	
	void Update ()
    {
        if(startWaves && currentWave <= waveClass.Length)
        {
            spawnDuration[currentWave] -= 1 * Time.deltaTime;
            if(spawnDuration[currentWave] <= 0)
            {
                int enemyid = Random.Range(0,waveClass[currentWave].enemys.Length);
                for (int i = 0; i < waveClass[currentWave].enemys.Length; i++)
                {
                    if(waveClass[currentWave].spawnEnemys[i] > 0 && i == enemyid)
                    {
                        SpawnEnemy(enemyid);
                        totalEnemys[currentWave] -= 1;
                        waveClass[currentWave].spawnEnemys[i] -= 1;
                        if(totalEnemys[currentWave] <= 0)
                        {
                            startWaves = false;
                        }
                    }
                }
                spawnDuration[currentWave] = spawnDurationReset[currentWave];
            }
        }
        if (!startWaves)
        {
            waveClass[currentWave].timeNextWave -= 1 * Time.deltaTime;
            if(waveClass[currentWave].timeNextWave <= 0 && currentWave <= waveClass.Length)
            {
                currentWave += 1;
                startWaves = true;
            }
        }
    }

    void SpawnEnemy(int enemyId)
    {
        Debug.Log(enemyId);
        for (int i = 0; i < waveClass[currentWave].objectPoolScript[enemyId].objects.Count; i++)
        {
            if (!waveClass[currentWave].objectPoolScript[enemyId].objects[i].activeInHierarchy)
            {
                if (waveClass[currentWave].customSpawn.Length != 0)
                {
                    int spawnLoc = Random.Range(0, waveClass[currentWave].customSpawn.Length);
                    waveClass[currentWave].objectPoolScript[enemyId].objects[i].transform.position = waveClass[currentWave].customSpawn[spawnLoc].transform.position;
                    waveClass[currentWave].objectPoolScript[enemyId].objects[i].transform.rotation = waveClass[currentWave].customSpawn[spawnLoc].transform.rotation;
                }
                else
                {
                    int spawnLoc = Random.Range(0, spawnLocation.Length);
                    waveClass[currentWave].objectPoolScript[enemyId].objects[i].transform.position = spawnLocation[spawnLoc].transform.position;
                    waveClass[currentWave].objectPoolScript[enemyId].objects[i].transform.rotation = spawnLocation[spawnLoc].transform.rotation;
                }
                waveClass[currentWave].objectPoolScript[enemyId].objects[i].SetActive(true);
                break;
            }
        }
    }
}

[System.Serializable]
public struct Waves
{
    [Header("========================")]
    public float waveSpawnDuration;
    public float timeNextWave;
    public GameObject[] enemys;
    public int[] spawnEnemys;
    public GameObject[] customSpawn;
    [HideInInspector]
    public ObjectPool_Script[] objectPoolScript;
}