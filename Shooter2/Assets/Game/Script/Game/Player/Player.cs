using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float health, healthRegen, armor, maxHealth, maxArmor;

    public Pickups[] pickupsClass;

    void Start ()
    {
        maxHealth = health;
	}
	
	void Update ()
    {
		if(health <= maxHealth)
        {
            health += healthRegen * Time.deltaTime;
        }
        if(health > maxHealth)
        { health = maxHealth; }
        if(armor > maxArmor)
        { armor = maxArmor; }
	}


    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < pickupsClass.Length; i++)
        {
            if(other.gameObject == pickupsClass[i].pickupObj)
            {
                if (pickupsClass[i].health && health <= maxHealth)
                {
                    health += pickupsClass[i].getAmount;
                    other.gameObject.SetActive(false);
                }
                if(pickupsClass[i].armor && armor <= maxArmor)
                {
                    armor += pickupsClass[i].getAmount;
                    other.gameObject.SetActive(false);
                }
            }
        }
    }
}

[System.Serializable]
public struct Pickups
{
    public GameObject pickupObj;
    public float getAmount;
    public bool health, armor;
}