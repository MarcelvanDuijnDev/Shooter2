using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene_GameSpeed : MonoBehaviour
{
    [SerializeField]
    private float gameSpeed;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            Time.timeScale = gameSpeed;
            other.gameObject.SetActive(false);
        }
    }
}
