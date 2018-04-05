using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObj : MonoBehaviour
{
    [Header("Waves")]
    public Respawn[] respawnClass;
    private float[] timers;

    void Start ()
    {
        timers = new float[respawnClass.Length];
        for (int i = 0; i < respawnClass.Length; i++)
        {
            timers[i] = respawnClass[i].respawnTime;
            if(respawnClass[i].startFalse)
            {
                respawnClass[i].gameObj.SetActive(false);
            }
        }
	}
	
	void Update ()
    {
        for (int i = 0; i < respawnClass.Length; i++)
        {
            if (!respawnClass[i].gameObj.activeSelf)
            {
                respawnClass[i].respawnTime -= 1 * Time.deltaTime;
                if(respawnClass[i].respawnTime <= 0)
                {
                    respawnClass[i].gameObj.SetActive(true);
                    respawnClass[i].respawnTime = timers[i];
                }
            }
        }
	}
}

[System.Serializable]
public struct Respawn
{
    public GameObject gameObj;
    public float respawnTime;
    public bool startFalse;
}