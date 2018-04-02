using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    WeaponNew weaponsScript;
    [SerializeField]private float health,armor,maxHealth,maxArmor,healthRegen;
    [Header("Pickups")]
    [SerializeField]private Pickups[] pickupsScript;

    void Start()
    {
        weaponsScript = this.gameObject.GetComponent<WeaponNew>();
    }

	void Update () 
    {
        if (health >= maxHealth)
        { health = maxHealth;}
        else
        { health += healthRegen * Time.deltaTime; }
        if (armor >= maxArmor)
        { armor = maxArmor;}
        //death
        if(health <= 0)
        { Debug.Log("Dead"); }
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
                    armor += pickupsScript[i].getAmount;
                    other.gameObject.SetActive(false);
                }
                if(pickupsScript[i].ammo)
                {
                    weaponsScript.GetAmmo(pickupsScript[i].getAmount);
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
    public int getAmount;
    public bool health,armor,ammo;
}