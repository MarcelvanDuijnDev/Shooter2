using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    [Header("Options")]
    public OptionsOnCollision[] optionsOnCollisionClass;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        for (int i = 0; i < optionsOnCollisionClass.Length; i++)
        {
            if (other.gameObject == optionsOnCollisionClass[i].collisionObject)
            {
                if (optionsOnCollisionClass[i].setActive) { optionsOnCollisionClass[i].targetObject.SetActive(true); }
                if (optionsOnCollisionClass[i].setFalse) { optionsOnCollisionClass[i].targetObject.SetActive(false); }
            }
        }
    }
}

[System.Serializable]
public struct OptionsOnCollision
{
    public bool setActive, setFalse;
    public GameObject targetObject;
    public GameObject collisionObject;
}