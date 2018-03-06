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
    public float[] spawnDuration;
    public int[] totalEnemys;
    

    void Start ()
    {
        totalWaves = waveClass.Length;
        spawnDuration = new float[waveClass.Length];
        totalEnemys = new int[waveClass.Length];
        for (int i = 0; i < waveClass.Length; i++)
        {
            for (int o = 0; o < waveClass[i].spawnEnemys.Length; o++)
            {
                totalEnemys[i] += waveClass[i].spawnEnemys[o];
            }
            spawnDuration[i] = waveClass[i].waveSpawnDuration / totalEnemys[i];
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
            //waveClass[currentWave].waveSpawnDuration -= 1 * Time.deltaTime;
            
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
