using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    private WeaponNew playerScript;
    [SerializeField]private Text healthText;
    [SerializeField]private GameObject goal;
    [SerializeField]private float health,speed;
    NavMeshAgent agent;

    private float maxHealth;

	void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.Find("Player");
        playerScript = (WeaponNew)goal.GetComponent(typeof(WeaponNew));
    }

    private void OnDisable()
    {
        health = maxHealth;
        //healthText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        maxHealth = health;
    }

    void FixedUpdate () 
    {
        agent.speed = speed;
        agent.destination = goal.transform.position; 

        if(health <= 0)
        {
            playerScript.kills += 1;
            this.gameObject.SetActive(false);
        }

        //healthText.transform.position = transform.position;
        //healthText.transform.rotation = transform.rotation;
        //healthText.text = health.ToString("F0") + "/" + maxHealth.ToString("F0");
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            health -= 25;
            other.gameObject.SetActive(false);
        }
    }

}
