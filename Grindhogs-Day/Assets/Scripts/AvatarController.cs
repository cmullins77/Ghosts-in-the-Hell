using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour {

	public float direction = 0;
	public bool isGrounded = false;

	Animator anim;
	SpriteRenderer sr;
	Rigidbody2D rb;
	AvatarStateManager asm;
	float distToGround = 0.2f;
	float speed = 1f;
	float thrust = 1f;

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
			asm.state = "idle";
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
			transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
		}
		else{
			anim.SetBool("run",false);
		}

		if(v_movement<0){
			anim.SetBool("crouch",true);
		}
		else{
			anim.SetBool("crouch",false);
		}

		if(v_movement>0 && isGrounded){
			anim.SetBool("jump",true);
			rb.velocity = Vector2.up * speed * thrust;
			anim.SetBool("flight",true);
		}

		//attack
		if (fire1){
			anim.SetTrigger("attack");
		}

		//reset old values
		float old_h_movement = h_movement; //this is a continuous value that should between switch frames
		GetNextInput(false);

		if(isMoving){
			h_movement = old_h_movement;
		}
	}

	void GetNextInput(bool isFresh){
		if(isFresh){
			h_movement = float.Parse(recSubstrings[1]);		
			v_movement = float.Parse(recSubstrings[2]);
			fire1 = bool.Parse(recSubstrings[3]);
		}
		else{
			h_movement = 0;		
			v_movement = 0;
			fire1 = false;
		}
	}

	void ExRec(){
		if(recQ.Count <= 0){
			matchFrame = -1;
			Destroy(gameObject);
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
}
