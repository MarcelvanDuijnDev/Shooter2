using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    [Header("Music Manager")]
    public MusicManager[] musicClass;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for (int i = 0; i < musicClass.Length; i++)
            {
                //
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for (int i = 0; i < musicClass.Length; i++)
            {
                //exit audio source
            }
        }
    }
}

[System.Serializable]
public struct MusicManager
{
    public float lerpMusicEnter, lerpMusicExit;
    public GameObject areaObj;
    public AudioClip audioClip;
    public AudioSource audioSource;
}