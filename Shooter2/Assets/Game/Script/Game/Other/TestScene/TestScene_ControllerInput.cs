using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene_ControllerInput : MonoBehaviour 
{
    public GameObject[] objects;
    public Vector3 pos1;
    public Vector3 pos2;
   
	void Update () 
    {
        if (Input.GetButton("AButton"))
        {objects[0].SetActive(true);}else
        {objects[0].SetActive(false);}
        if (Input.GetButton("XButton"))
        {objects[1].SetActive(true);}else
        {objects[1].SetActive(false);}
        if (Input.GetButton("BButton"))
        {objects[2].SetActive(true);}else
        {objects[2].SetActive(false);}
        if (Input.GetButton("YButton"))
        {objects[3].SetActive(true);}else
        {objects[3].SetActive(false);}
        if (Input.GetButton("L1Button"))
        {Debug.Log("L1"); objects[6].SetActive(true);}else
        {objects[6].SetActive(false);}
        if (Input.GetButton("L2Button"))
        {Debug.Log("L2"); objects[7].SetActive(true);}else
        {objects[7].SetActive(false);}
        if (Input.GetButton("R1Button"))
        {Debug.Log("R1"); objects[8].SetActive(true);}else
        {objects[8].SetActive(false);}
        if (Input.GetButton("R2Button"))
        {Debug.Log("R2"); objects[9].SetActive(true);}else
        {objects[9].SetActive(false);}


        pos1.x = Input.GetAxis("LeftJoystickHorizontal");
        pos1.z = -Input.GetAxis("LeftJoystickVertical");

        pos2.x = Input.GetAxis("RightJoystickHorizontal");
        pos2.z = -Input.GetAxis("RightJoystickVertical");

        objects[4].transform.position += pos1;
        objects[5].transform.position += pos2;
	}
}
