using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    [SerializeField]private float health,armor,maxHealth,maxArmor;

    [SerializeField]private Pickups[] pickupsScript;

	void Update () 
    {
        if (health >= maxHealth)
        { health = maxHealth;}
        if (armor >= maxArmor)
        { armor = maxArmor;}
	}

    public void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < pickupsScript.Length; i++)
        {
            if (other.gameObject == pickupsScript[i].pickupObject)
            {
                if (health <= maxHealth && pickupsScript[i].health)
                {
                    health += pickupsScript[i].getAmount;
                    other.gameObject.SetActive(false);
                }
                if (armor <= maxArmor && pickupsScript[i].armor)
                {
                    health += pickupsScript[i].getAmount;
                    other.gameObject.SetActive(false);
                }
            }
        }
    }
}


[System.Serializable]
public struct Pickups
{
    public GameObject pickupObject;
    public float getAmount;
    public bool health,armor;
}