using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float timeStart;
    public GameObject[] spawnLocation;
    public float[] timeNewWave;

    [Header("Waves")]
    public Waves[] waveClass;

    private int currentWave,totalWaves;
    private bool startWaves;
    private float spawnDuration;

    void Start ()
    {
        totalWaves = waveClass.Length;
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
            waveClass[currentWave].waveSpawnDuration -= 1 * Time.deltaTime;
            
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
