using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWave : MonoBehaviour
{
    private Wave waveScript;
    [Header("Total Waves")]
    [SerializeField]private int waveAmount;

    [Header("EnemyPools")]
    [SerializeField]private GameObject[] enemyPools;

    [Header("Add on wave")]
    [SerializeField]private WaveAdd[] waveAddScript;

    [Space(20)]
    [Header("Wave Settings")]
    [SerializeField]private float waveDuration;
    [SerializeField]private float timeNextWave;
    [SerializeField]private bool killAll;

    private GameObject[] enemyPoolsReset;
    private float timer;
    private bool check;

    void Start ()
    {
        enemyPoolsReset = new GameObject[enemyPools.Length];
        for (int i = 0; i < enemyPoolsReset.Length; i++)
        {
            enemyPoolsReset[i] = enemyPools[i];
        }
        waveScript = this.gameObject.GetComponent<Wave>();
        waveScript.startTimer = 4;

        waveScript.waveClass = new Waves[waveAmount];
        for (int i = 0; i < waveAmount; i++)
        {
            //Add on wave
            for (int o = 0; o < waveAddScript.Length; o++)
            {
                if (waveAddScript[o].wave -1 == i)
                {
                    enemyPools = new GameObject[enemyPools.Length + 1];
                    for (int p = 0; p < enemyPools.Length - 1; p++)
                    {
                        enemyPools[p] = enemyPoolsReset[p];
                    }
                    enemyPools[enemyPools.Length - 1] = waveAddScript[o].addedObject;
                    enemyPoolsReset = new GameObject[enemyPools.Length];
                    for (int p = 0; p < enemyPoolsReset.Length; p++)
                    {
                        enemyPoolsReset[p] = enemyPools[p];
                    }
                }
            }
            //
            waveScript.waveClass[i].enemys = new GameObject[enemyPools.Length];
            waveScript.waveClass[i].spawnEnemys = new int[enemyPools.Length];
            waveScript.waveClass[i].customSpawn = new GameObject[0];
            for (int o = 0; o < waveScript.waveClass[i].enemys.Length; o++)
            {
                int number = i + 2; //encrease count
                waveScript.waveClass[i].enemys[o] = enemyPools[o];
                waveScript.waveClass[i].spawnEnemys[o] = number;
            }
            waveScript.waveClass[i].waveSpawnDuration = waveDuration;
            waveScript.waveClass[i].timeNextWave = timeNextWave;
            waveScript.waveClass[i].killAll = killAll;

            //Debug
            float amount = 0;
            for (int o = 0; o < waveScript.waveClass[i].spawnEnemys.Length; o++)
            {
                amount += waveScript.waveClass[i].spawnEnemys[o];
            }
            //Debug.Log(amount);
        }
    }
	
	void Update ()
    {
        timer -= 1 * Time.deltaTime;
        if(timer <= 2 && !check)
        {
            waveScript.GetInfo();
            check = true;
        }
	}
}

[System.Serializable]
public struct WaveAdd
{
    public GameObject addedObject;
    public int wave;
}