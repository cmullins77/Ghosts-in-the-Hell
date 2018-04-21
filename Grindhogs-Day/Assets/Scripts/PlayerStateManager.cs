using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour {

	public string state = "idle";
	float speed = 1.5f;
	float thrust = 2f;

	GameObject weapon;
	PlayerController pc;
	Rigidbody2D rb;
	Animator anim;
	float force = 50;
	float airLimit = 0.3f; //maximum time on upthrust for jump
	float airTimer = 0f;

	// Use this for initialization
	void Start () {
		pc = GetComponent<PlayerController>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		// weapon = this.gameObject.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		//character flip
		if(pc.direction<0){
			transform.localScale = new Vector3 (-1,1,1);
		}
		if(pc.direction>0){
			transform.localScale = new Vector3 (1,1,1);
		}

		//state manager
		switch(state){
			case "idle":
				anim.SetBool("flight",false);
				anim.SetBool("carry",false);
				airTimer = 0;
				break;			
			case "flight":
				anim.SetBool("flight",true);
				if(anim.GetBool("jump")){
					airTimer += Time.deltaTime;
					if(airTimer<airLimit){
						rb.velocity = Vector2.up * speed * thrust;
					}
					else{
						airTimer = 0;
						rb.velocity = Vector2.zero;
						anim.SetBool("jump",false);
					}	
				}
				break;			
			case "carry":			
				anim.SetBool("carry",true);
				break;
		}
	}
}
