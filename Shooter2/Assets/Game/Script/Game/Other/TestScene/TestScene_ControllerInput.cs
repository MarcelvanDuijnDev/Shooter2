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

        if (Input.GetButton("DPadUp"))
        { Debug.Log("DPadUp"); objects[10].SetActive(true); }else
        { objects[10].SetActive(false); }
        if (Input.GetButton("DPadDown"))
        { Debug.Log("DPadDown"); objects[11].SetActive(true); }else
        { objects[11].SetActive(false); }
        if (Input.GetButton("DPadLeft"))
        { Debug.Log("DPadLeft"); objects[12].SetActive(true); }else
        { objects[12].SetActive(false); }
        if (Input.GetButton("DPadRight"))
        { Debug.Log("DPadRight"); objects[13].SetActive(true); }else
        { objects[13].SetActive(false); }

        if (Input.GetButton("Select"))
        { Debug.Log("Select"); objects[14].SetActive(true); }else
        { objects[14].SetActive(false); }
        if (Input.GetButton("Start"))
        { Debug.Log("Start"); objects[15].SetActive(true); }else
        { objects[15].SetActive(false); }

        if (Input.GetButton("XboxButton"))
        { Debug.Log("XboxButton"); objects[16].SetActive(true); }else
        { objects[16].SetActive(false); }

        if (Input.GetButton("JoyStickClickLeft"))
        { Debug.Log("JoyStickClickLeft");}
        if (Input.GetButton("JoyStickClickRight"))
        { Debug.Log("JoyStickClickRight"); }



        pos1.x = Input.GetAxis("LeftJoystickHorizontal");
        pos1.z = -Input.GetAxis("LeftJoystickVertical");

        pos2.x = Input.GetAxis("RightJoystickHorizontal");
        pos2.z = -Input.GetAxis("RightJoystickVertical");

        objects[4].transform.position += pos1 / 5;
        objects[5].transform.position += pos2 / 5;
	}
}
