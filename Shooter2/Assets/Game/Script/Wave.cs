using System.Collections;
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
                spawnDuration[currentWave] = spawnDurationReset[currentWave];
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
    



}
