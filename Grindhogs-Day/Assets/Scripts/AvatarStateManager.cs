using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarStateManager : MonoBehaviour {

	public string state = "idle";
	public float speed = 1.5f;
	public float thrust = 2f;
	public Transform carrySpot;
	public Transform liftSpot;

	GameObject weapon;
	GameObject carryObject;
	AvatarController ac;
	Rigidbody2D rb;
	Animator anim;
	float force = 50;
	float airLimit = 0.3f; //maximum time on upthrust for jump
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
					if(ac.isLifting)
						carryObject.transform.position = liftSpot.position;
					else
						carryObject.transform.position = carrySpot.position;
				}
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

}
