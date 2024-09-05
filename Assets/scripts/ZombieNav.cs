using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieNav : MonoBehaviour {
	[HideInInspector]public NavMeshAgent agent;
	public float Radius;
	Vector3 Dir;
	[SerializeField]float _zombieHealth = 100;
	public Transform Player;
	public LayerMask Whatisground, WhatisPlayer;
	public float sightRange;
	public bool playerInSightRange;
	player hit;
   
    private void Start () {
		agent = GetComponent <NavMeshAgent > ();
		Player = GameObject.FindWithTag("Player").transform;
    }
	void RandomPoint (){
		Dir = new Vector3 (Random.Range (-Radius + transform.position.x, Radius + transform.position.x), 0, Random.Range (-Radius + transform.position.z, Radius + transform.position.z));
	}

	void playerchase()
	{
		agent.SetDestination(Player.position);
	}
	private void Update () {
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatisPlayer);
		if (!playerInSightRange)
		{
			if (!agent.hasPath)
			{
				RandomPoint();
				agent.SetDestination(Dir);
			}

		}
		if (playerInSightRange) {
		
		playerchase();
		}
	

        if (_zombieHealth <=0)
		{
			Destroy(this.gameObject);
		}
	}
	#if UNITY_EDITOR 
	private void OnDrawGizmos(){
		Gizmos.DrawWireSphere (transform.position, Radius);
	}
	#endif 
	public void takeDamage(float damage)
	{
		_zombieHealth -=damage;
		Debug.Log(_zombieHealth);
	}
    private void OnCollisionEnter(Collision col)

    {
        
        if (col.gameObject.tag == "Player")
		{
			hit = FindObjectOfType<player>();
            hit.Getdamage(10);
		}
    }
}