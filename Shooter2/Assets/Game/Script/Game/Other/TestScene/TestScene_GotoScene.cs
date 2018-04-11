using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TestScene_GotoScene : MonoBehaviour {

    [SerializeField]private int sceneIndex;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
