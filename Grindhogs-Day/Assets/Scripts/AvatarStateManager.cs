using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarStateManager : MonoBehaviour {

	public string state = "idle";
	public float speed = 1.5f;
	public float thrust = 2f;

	GameObject weapon;
	AvatarController ac;
	Rigidbody2D rb;
	Animator anim;
	float force = 50;
	float airLimit = 0.5f; //maximum time on upthrust for jump
	float airTimer = 0f;

	// Use this for initialization
	void Start () {
		ac = GetComponent<AvatarController>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		// weapon = this.gameObject.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		//character flip
		if(ac.direction<0){
			transform.localScale = new Vector3 (-1,1,1);
		}
		if(ac.direction>0){
			transform.localScale = new Vector3 (1,1,1);
		}

		//state manager
		switch(state){
			case "idle":
				anim.SetBool("flight",false);
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
		}
	}

}
