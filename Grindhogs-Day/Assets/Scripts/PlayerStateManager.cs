using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour {

	public string state = "idle";
	float speed = 1.5f;
	float thrust = 1f;
	public Transform carrySpot;
	public Transform liftSpot;

	GameObject weapon;
	GameObject carryObject;
	PlayerController pc;
	SpawnManager sMgr;
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
		sMgr = GameObject.Find("DeathGod").GetComponent<SpawnManager>();
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
				if(carryObject!=null){
					Physics2D.IgnoreCollision(GetComponent<Collider2D>(), carryObject.GetComponent<Collider2D>(), false);
					carryObject.transform.parent = null; //detach object from child;
					carryObject = null;
				}
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
				carryObject = GetChildObject();
				if(carryObject!=null){
					Physics2D.IgnoreCollision(GetComponent<Collider2D>(), carryObject.GetComponent<Collider2D>());
					if(pc.isLifting)
						carryObject.transform.position = liftSpot.position;
					else
						carryObject.transform.position = carrySpot.position;
				}
				break;
			case "dead":
				state = "dead";
				break;
		}
	}

	GameObject GetChildObject(){
		foreach (Transform child in transform){
			if(child.tag == "Object"){
				return child.gameObject;
			}
		}
		return null;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Fatal"){
			KillPlayer();
		}
	}

	void KillPlayer(){
		if(state!="dead"){
			state = "dead";
			anim.SetTrigger("die");
			sMgr.KillPlayer(pc.GetInputQ());
			pc.enabled = false; //gotta stay dead	
		}
	}
}
