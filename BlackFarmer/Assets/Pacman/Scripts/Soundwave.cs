using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundwave : MonoBehaviour {

	public Vector3 direction;
	public float distanceToLive = 5f;

	private float speed = 10.0f;
	private float distanceTravelled;
	private GameManager _gm;
	private Vector3 initPos;

	// Use this for initialization
	void Start () {
		string name = GetComponent<SpriteRenderer>().sprite.name;
		if(name[name.Length-1] == '0' )	direction = Vector3.left;
		if(name[name.Length-1] == '1' )	direction = Vector3.right;
		if(name[name.Length-1] == '2' )	direction = Vector3.up;
		if(name[name.Length-1] == '3' )	direction = Vector3.down;
		
		speed = GameObject.Find("axeman").GetComponent<PlayerController>().speed * 50;
	    _gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
	    initPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(direction * Time.deltaTime * speed);
		distanceTravelled = Vector3.Distance(transform.position, initPos);
		if(distanceTravelled>distanceToLive){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		_gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
		
		// Increase the score everytime you scare pacman
		if(other.name == "pacman")
		{
			other.GetComponent<GhostMove>().Scare();
			GameManager.score += 100;
			Destroy(gameObject);
		}

		if(other.name == "clyde" || other.name == "blinky" || other.name == "inky" || other.name == "pinky")
		{
			other.GetComponent<Animator>().SetBool("Bust",true);
			_gm.LoseLife();
		}

				// Don't want sound waves passing through walls
		if(other.name != "axeman" && !other.name.Contains("wave") ){
			// print(distanceTravelled+" "+other.name);
			Destroy(gameObject);
		}

	}
}
