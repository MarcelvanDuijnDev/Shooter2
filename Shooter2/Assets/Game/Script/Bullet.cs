using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    float timer = 10;
	
	void Update () 
    {
        timer -= 1 * Time.deltaTime;
        if (timer <= 0)
        {
            this.gameObject.SetActive(false);
        }
	}

    void OnEnable()
    {
        timer = 10;
    }

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
    }
}
