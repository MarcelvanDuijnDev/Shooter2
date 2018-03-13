using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    [SerializeField]private Text healthText;
    [SerializeField]private Transform goal;
    [SerializeField]private float health,speed;
    NavMeshAgent agent;

    private float maxHealth;

	void Start () 
    {
       maxHealth = health;
       agent = GetComponent<NavMeshAgent>();
	}

    private void OnDisable()
    {
        healthText.gameObject.SetActive(false);
    }

    void FixedUpdate () 
    {
        agent.speed = speed;
        agent.destination = goal.position; 

        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }

        healthText.transform.position = transform.position;
        healthText.transform.rotation = transform.rotation;
        healthText.text = health.ToString("F0") + "/" + maxHealth.ToString("F0");
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
