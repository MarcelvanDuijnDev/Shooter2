using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    [SerializeField]private Transform goal;
    [SerializeField]private float health,speed;
    NavMeshAgent agent;

	void Start () 
    {
       agent = GetComponent<NavMeshAgent>();
	}
	
	void FixedUpdate () 
    {
        agent.speed = speed;
        agent.destination = goal.position; 

        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            health -= 25;
        }
    }

}
