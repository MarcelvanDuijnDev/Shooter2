using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene_SpawnEnemy : MonoBehaviour {

    //Enemy Spawn
    public GameObject enemySpawn;
    public GameObject enemyPrefab;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collisio");
        if (other.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("Collision");
            other.gameObject.SetActive(false);
            GameObject obj = (GameObject)Instantiate(enemyPrefab);
            obj.transform.position = enemySpawn.transform.position;
        }
    }
}
