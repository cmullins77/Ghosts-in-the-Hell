using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float direction = 0;
	public bool isGrounded = false;
	public GameObject pastLife;

	Animator anim;
	SpriteRenderer sr;
	Rigidbody2D rb;
	PlayerStateManager psm;
	float distToGround=0.2f;
	float speed = 1f;
	float thrust = 1f;

	double frameOffset = 0;
	double lastFrameCount = 0;
	Vector2 initPos;
	Quaternion initRot;
	GameObject lastLife;

	float old_h_movement = 0; //from last frame
	float old_v_movement = 0; //from last frame
	bool old_fire1 = false; //from last frame

	Queue inputQ = new Queue(); //format: frame#horizontal#vertical#fire

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		psm = GetComponent<PlayerStateManager>();
		rb = GetComponent<Rigidbody2D>();

		initPos = transform.position;
		initRot = transform.rotation;

		lastFrameCount = Time.frameCount; //initialFrame
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -1 * Vector2.up);

		if(hit.distance < distToGround){
			psm.state = "idle";
			isGrounded=true;
			anim.SetBool("jump",false);
		}
		else{
			psm.state = "flight";
			isGrounded=false;
		}

		//movement
		float h_movement = Input.GetAxis("Horizontal");		
		float v_movement = Input.GetAxis("Vertical");

		h_movement = h_movement < 0 ? -1 : (h_movement > 0 ? 1 : 0); //normalise values to ceil and floor
		v_movement = v_movement < 0 ? -1 : (v_movement > 0 ? 1 : 0); //normalise values to ceil and floor

		direction = h_movement < 0 ? -1 : (h_movement > 0 ? 1 : 0);

		if (h_movement!=0 && v_movement>=0){
			if(isGrounded)
				anim.SetBool("run",true);
			// Vector2 moveOff = Vector2.right * direction * speed * Time.deltaTime;
			// print("is translating: "+ moveOff.ToString("F4"));
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
		bool fire1 = Input.GetButtonDown("Fire1");
		if (fire1){
			anim.SetTrigger("attack");
		}

		//record only frames where there is input
		 if (isInputChange(h_movement,v_movement,fire1)){
		 	frameOffset = Time.frameCount - lastFrameCount;
		 	lastFrameCount = Time.frameCount;
		 	string inputString = frameOffset+"#"+h_movement+"#"+v_movement+"#"+fire1;
		 	inputQ.Enqueue(inputString);
		 	// print(inputString);
		 }

		 //record this frames values to compare with the next
		 old_fire1 = fire1;
		 old_h_movement = h_movement;
		 old_v_movement = v_movement;

		 //hotkey for instantiating past life
		 if (Input.GetKeyDown(KeyCode.I)){
		 	lastLife = Instantiate(pastLife,initPos,initRot);
		 	lastLife.GetComponent<AvatarController>().InitiateQ(inputQ);
		 	inputQ = new Queue(); //flush old queue;
		 }
	}

	bool isInputChange(float h_movement, float v_movement,bool fire1){
		if(h_movement!=old_h_movement)
			return true;
		if(v_movement!=old_v_movement)
			return true;
		if(fire1!=old_fire1)
			return true;

		return false;
	}

}
