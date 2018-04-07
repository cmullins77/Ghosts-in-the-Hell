using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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
			Destroy(gameObject);
		}
	}
}
