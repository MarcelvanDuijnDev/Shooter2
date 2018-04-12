using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene_DeleteTime : MonoBehaviour {
    [SerializeField]
    private float timer;
    void Start()
    {
        Destroy(this.gameObject, timer);
    }
}
