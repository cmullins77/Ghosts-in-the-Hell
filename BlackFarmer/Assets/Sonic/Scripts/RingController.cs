using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour {

	public GameObject ring_spawner;

	private float lo_threshold=0;
	private float hi_threshold=0;

	// Use this for initialization
	void Start () {	

		if(gameObject.name == "ring_blue(Clone)"){
			lo_threshold=0;
			hi_threshold=33;
		}
		if(gameObject.name == "ring_yellow(Clone)"){
			lo_threshold=33;
			hi_threshold=66;
		}
		if(gameObject.name == "ring_red(Clone)"){
			lo_threshold=66;
			hi_threshold=100;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// Increase the score everytime you scare pacman
		if(other.name == "cart")
		{
			// GameManager.score += 100;
			float cartVelocity = other.GetComponent<Rigidbody2D>().velocity.magnitude;
			if(cartVelocity>lo_threshold && cartVelocity<hi_threshold){
				// print("lo: "+lo_threshold+", hi: "+hi_threshold+", vel: "+cartVelocity);
				RingSpawner.ring_count -= 1;
				ring_spawner.GetComponent<RingSpawner>().KillPing();
				Destroy(gameObject);	
			}
		}
	}
}
