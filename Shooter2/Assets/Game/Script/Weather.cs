using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    [SerializeField]private GameObject playerObj,rainObj,sunObj;
    [SerializeField]private Vector3 time;
    [SerializeField]private int daysInYear;
    public int currentYear,currentDay;
    [Range(0, 1)][SerializeField]private float weatherState;

    private float totalTime,speed,lightIntensity,rainIntesity,currentRainIntesity,timeCalculate;
    private Light sunLight;
    private ParticleSystem rainParticles;
    

	void Start () 
    {
        sunLight = sunObj.GetComponent<Light>();
        rainParticles = rainObj.GetComponent<ParticleSystem>();
        rainObj.SetActive(false);

        totalTime += time.x * 60 * 60;
        totalTime += time.y * 60;
        totalTime += time.z;
        timeCalculate = totalTime;
        speed = 360/totalTime;
        currentDay = 1;
	}
	
	void Update () 
    {
        sunObj.transform.Rotate(Vector3.right * speed * Time.deltaTime);
        rainObj.transform.position = new Vector3(playerObj.transform.position.x,playerObj.transform.position.y + 10,playerObj.transform.position.z);

        //Calculate Day/Year
        timeCalculate -= 1 * Time.deltaTime;
        if(timeCalculate <= 0)
        {
            currentDay += 1;
            timeCalculate = totalTime;
        }
        if(currentDay >= daysInYear)
        {
            currentYear += 1;
            currentDay = 1;
        }

        //Weather state
        if (weatherState < 0.1f){lightIntensity = 0; rainIntesity = 200;}
        if (weatherState > 0.25f){lightIntensity = 0.25f; rainIntesity = 150;}
        if (weatherState > 0.5f){lightIntensity = 0.50f;rainObj.SetActive(false);}else{rainObj.SetActive(true); rainIntesity = 100;}
        if (weatherState > 0.75f){lightIntensity = 0.75f;}
        if (weatherState > 0.9f){lightIntensity = 1;}
        if (sunLight.intensity < lightIntensity){sunLight.intensity += 0.1f * Time.deltaTime;}
        if (sunLight.intensity > lightIntensity){sunLight.intensity -= 0.1f * Time.deltaTime;}
        //rain emmision rate
	}
}
