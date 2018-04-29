using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AvatarController : MonoBehaviour {

	public float direction = 0;
	public bool isGrounded = false;
	public bool isCarrying = false;
	public bool isLifting = false;

	Animator anim;
	SpriteRenderer sr;
	Rigidbody2D rb;
	AvatarStateManager asm;
	float distToGround = 0.2f;
	float speed = 1f;
	float thrust = 2.5f;
	float slip = 1f;

	double frameOffset = 0;
	double lastFrameCount = 0;
	double matchFrame = -1; //frame to be matched
	string recString = "";
	string[] recSubstrings;

	float h_movement = 0;		
	float v_movement = 0;
	bool fire1 = false;
	bool fire2 = false;
	bool isMoving = true; //continuous value must be executed every frame

	Queue recQ = new Queue(); //format: frame#horizontal#vertical#fire

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		asm = GetComponent<AvatarStateManager>();
		rb = GetComponent<Rigidbody2D>();

		lastFrameCount = Time.frameCount; //initialFrame
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit2D hit = Physics2D.Raycast(transform.position, -1 * Vector2.up);

		if(hit.distance < distToGround){
			if(!isCarrying)
				asm.state = "idle";
			else
				asm.state = "carry";
			isGrounded=true;
			anim.SetBool("jump",false);
		}
		else{
			asm.state = "flight";
			isGrounded=false;
		}

		Replay();
	}

	void Replay(){

		//movement
		if(matchFrame == Time.frameCount - lastFrameCount){
			if(recSubstrings.Length==2)
				asm.KillPlayer(); // seize replay after last action frame
			lastFrameCount = Time.frameCount;
			GetNextInput(true);
			ExRec();
		}

		if(h_movement!=0)
			isMoving=true;
		else
			isMoving=false;

		direction = h_movement < 0 ? -1 : (h_movement > 0 ? 1 : 0);

		if (h_movement!=0 && v_movement>=0){
			if(isGrounded)
				anim.SetBool("run",true);
			float modifier = 1f;
			if(isCarrying)
				modifier = 0.5f;	//slowdown movement speed while walking
			if(isLifting)
				modifier = 0;	//no movement when lifting
			transform.Translate(Vector2.right * direction * speed * Time.deltaTime * modifier);
		}
		else{
			anim.SetBool("run",false);
		}

		//crouch
		if(v_movement<0){
			anim.SetBool("crouch",true);
			//slide
			if(h_movement!=0 && !isCarrying){
				anim.SetTrigger("slide");
				rb.velocity = Vector2.right * direction * speed * slip;
			}
			if(isCarrying){
				anim.SetBool("lift",false);
				isLifting = false;
			}
		}
		else{
			anim.SetBool("crouch",false);
		}

		//jump
		if(v_movement>0){
			if(isGrounded && !isCarrying){
				anim.SetBool("jump",true);
				rb.velocity = Vector2.up * speed * thrust;
				anim.SetBool("flight",true);
			}
			if(isCarrying){
				anim.SetBool("lift",true);
				isLifting = true;
			}
		}

		//use
		if (fire2){
			if(isCarrying && !isLifting) //can't drop object while lifting
				isCarrying=false; //drops object
			else
				anim.SetTrigger("use");
		}

		//attack
		if (fire1){
			anim.SetTrigger("attack");
		}

		//reset old values
		float old_h_movement = h_movement; //this is a continuous value that should persist between switch frames
		float old_v_movement = v_movement; //this is a continuous value that may persist between switch frames
		GetNextInput(false);

		if(isMoving){
			h_movement = old_h_movement;
		}
		if(old_v_movement!=0){
			v_movement = old_v_movement;
		}
	}

	void GetNextInput(bool isFresh){
		try{	
			if(isFresh){
				h_movement = float.Parse(recSubstrings[1]);		
				v_movement = float.Parse(recSubstrings[2]);
				fire1 = bool.Parse(recSubstrings[3]);
				fire2 = bool.Parse(recSubstrings[4]);
			}
			else{
				h_movement = 0;		
				v_movement = 0;
				fire1 = false;
				fire2 = false;
			}
		}
		catch(FormatException e){
			//Parse exception
		}
	}

	void ExRec(){
		if(recQ.Count <= 0){
			matchFrame = -1;
			return;
		}
		recString = (string) recQ.Dequeue();
		char delimiter = '#';
		recSubstrings = recString.Split(delimiter);
		matchFrame = double.Parse(recSubstrings[0]);

		// print(recString);
	}

	public void InitiateQ(Queue inputQ){
		recQ = new Queue(inputQ);

		ExRec();
	}


	public void SetAlpha(float opacity){
		Color tmp = GetComponent<SpriteRenderer>().color;
 		tmp.a = opacity;
 		gameObject.GetComponent<SpriteRenderer>().color = tmp;
	}

	public float GetThrust(){
		return thrust;
	}

}
