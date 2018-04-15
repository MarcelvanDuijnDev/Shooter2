using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour 
{
    [SerializeField]private bool state_TurnOn,state_TurnOff,state_Active;
    [SerializeField]private Transform rotator_1,rotator_2,blades,lookPoint,cameraObj;
    private Vector3 rotator_1_Rotation,rotator_2_Rotation,blades_Rotation;

    [Header("Simulation")]
    [Range(-1,1)]public float directionX;
    [Range(-1,1)]public float directionZ;
    [Range(0,1)]public float speed;

	void Update () 
    {
        if (state_Active)
        {
            blades_Rotation.y = speed * 5000;

            rotator_2_Rotation.x = directionX * 80 + 90;
            rotator_1_Rotation.x = directionX * 80;
            rotator_1_Rotation.z = directionZ * -80;

            rotator_1.transform.eulerAngles = rotator_1_Rotation;
            rotator_2.transform.eulerAngles = rotator_2_Rotation;
            blades.transform.Rotate(blades_Rotation * Time.deltaTime);

            cameraObj.transform.LookAt(lookPoint);
        }
        if (state_TurnOff)
        {
            if (blades_Rotation.y > 0){blades_Rotation.y -= 300 * Time.deltaTime;}else{blades_Rotation.y = 0;}
            if (rotator_2_Rotation.x > 0){rotator_2_Rotation.x -= 10 * Time.deltaTime;}
            if (rotator_2_Rotation.x < 0){rotator_2_Rotation.x += 10 * Time.deltaTime;}
            if (rotator_1_Rotation.x > 0){rotator_1_Rotation.x -= 5 * Time.deltaTime;}
            if (rotator_1_Rotation.x < 0){rotator_1_Rotation.x += 5 * Time.deltaTime;}
            if (rotator_1_Rotation.z > 0){rotator_1_Rotation.z -= 5 * Time.deltaTime;}
            if (rotator_1_Rotation.z < 0){rotator_1_Rotation.z += 5 * Time.deltaTime;}

            rotator_1.transform.eulerAngles = rotator_1_Rotation;
            rotator_2.transform.eulerAngles = rotator_2_Rotation;
            blades.transform.Rotate(blades_Rotation * Time.deltaTime);
        }
        if (state_TurnOn)
        {
            if (blades_Rotation.y <= speed * 5000){blades_Rotation.y += 200 * Time.deltaTime;}
            if (rotator_2_Rotation.x < directionX * 80 + 90)
            {
                rotator_2_Rotation.x += 8 * Time.deltaTime;
            }
            rotator_1_Rotation.x = directionX * 80;
            rotator_1_Rotation.z = directionZ * -80;

            rotator_1.transform.eulerAngles = rotator_1_Rotation;
            rotator_2.transform.eulerAngles = rotator_2_Rotation;
            blades.transform.Rotate(blades_Rotation * Time.deltaTime);

            //cameraObj.transform.LookAt(lookPoint);
        }
	}
}






