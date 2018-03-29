using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWave : MonoBehaviour
{
    Wave waveScript;

    [SerializeField]private GameObject[] enemyPools;
    [SerializeField]private int waveAmount;

    [Header("Wave Settings")]
    [SerializeField]private float waveDuration;
    [SerializeField]private float timeNextWave;

    private float timer;
    private bool check;

    void Start ()
    {
        waveScript = this.gameObject.GetComponent<Wave>();
        waveScript.startTimer = 4;

        waveScript.waveClass = new Waves[waveAmount];
        for (int i = 0; i < waveAmount; i++)
        {
            waveScript.waveClass[i].enemys = new GameObject[enemyPools.Length];
            waveScript.waveClass[i].spawnEnemys = new int[enemyPools.Length];
            waveScript.waveClass[i].customSpawn = new GameObject[0];
            for (int o = 0; o < waveScript.waveClass[i].enemys.Length; o++)
            {
                int number = Random.Range(1, i * 2);
                waveScript.waveClass[i].enemys[o] = enemyPools[o];
                waveScript.waveClass[i].spawnEnemys[o] = number;
            }
            waveScript.waveClass[i].waveSpawnDuration = waveDuration;
            waveScript.waveClass[i].timeNextWave = timeNextWave;
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
