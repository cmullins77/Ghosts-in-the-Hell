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
	float distToGround;
	float speed = 2f;
	float thrust = 2f;

	double frameOffset = 0;
	double lastFrameCount = 0;
	double matchFrame = -1; //frame to be matched
	string recString = "";
	string[] recSubstrings;


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

		if(hit.distance < 0.7){
			asm.state = "idle";
			isGrounded=true;
			anim.SetBool("jump",false);
		}
		else{
			asm.state = "flight";
			isGrounded=false;
		}

		// print(recString+" "+matchFrame+" "+Time.frameCount);
		if(matchFrame == Time.frameCount - lastFrameCount){
			lastFrameCount = Time.frameCount;
			Replay();
			ExRec();
		}
	}

	void Replay(){
		//movement
		float h_movement = float.Parse(recSubstrings[1]);		
		float v_movement = float.Parse(recSubstrings[2]);

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
		if (bool.Parse(recSubstrings[3])){
			anim.SetTrigger("attack");
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
	}

	public void InitiateQ(Queue inputQ){
		recQ = new Queue(inputQ);

		ExRec();
	}
}
