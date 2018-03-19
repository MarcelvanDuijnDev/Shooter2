﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float timeStart;
    public GameObject[] spawnLocation;

    [Header("Waves")]
    public Waves[] waveClass;

    private int currentWave,totalWaves;
    private bool startWaves;
    private float[] spawnDuration;
    private int[] totalEnemys;
    private float[] spawnDurationReset;
    

    void Start ()
    {
        for (int i = 0; i < waveClass.Length; i++)
        {
            for (int o = 0; o < waveClass[i].enemys.Length; o++)
            {
                waveClass[i].objectPoolScript = new ObjectPool_Script[waveClass[i].enemys.Length];
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
        timeStart -= 1 * Time.deltaTime;
        if(timeStart <= 0)
        {
            startWaves = true;
        }
        if(startWaves)
        {
            spawnDuration[currentWave] -= 1 * Time.deltaTime;
            if(spawnDuration[currentWave] <= 0)
            {
                SpawnEnemy(0);
                spawnDuration[currentWave] = spawnDurationReset[currentWave];
            }
        }
    }

    void SpawnEnemy(int enemyId)
    {
        for (int i = 0; i < waveClass[currentWave].objectPoolScript[enemyId].objects.Count; i++)
        {
            if (!waveClass[currentWave].objectPoolScript[enemyId].objects[i].activeInHierarchy)
            {
                int spawnLoc = Random.Range(0, spawnLocation.Length);
                waveClass[currentWave].objectPoolScript[enemyId].objects[i].transform.position = spawnLocation[spawnLoc].transform.position;
                waveClass[currentWave].objectPoolScript[enemyId].objects[i].transform.rotation = spawnLocation[spawnLoc].transform.rotation;
                waveClass[currentWave].objectPoolScript[enemyId].objects[i].SetActive(true);
                break;
            }
        }
    }
}

[System.Serializable]
public class Waves
{
    public float waveSpawnDuration;
    public GameObject[] enemys;
    public int[] spawnEnemys;

    [HideInInspector]
    public ObjectPool_Script[] objectPoolScript;


}