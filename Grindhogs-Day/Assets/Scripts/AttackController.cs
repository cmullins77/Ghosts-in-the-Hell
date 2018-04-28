using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {

	Rigidbody2D hit_rb;
	Vector2 hit_position;
	Vector2 target_position;
	float hit_limit = 0.3f;
	float time_elapsed = 0f;
	float force = 3f;
	float direction = 0;
	float distance = 1f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(hit_rb!=null){
			hit_position = hit_rb.gameObject.transform.position;
			Vector2 traj = (target_position - hit_position).normalized;
			float diff = Vector2.Distance(target_position,hit_position);
			if(diff>0.1){	
				hit_rb.MovePosition(hit_position + traj * force * Time.deltaTime);
			}
			else{
				target_position = hit_rb.gameObject.transform.position;
				hit_rb = null;
			}
		}
		else{
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag=="Object"){
        	hit_rb = other.gameObject.GetComponent<Rigidbody2D>();
        	hit_position = other.transform.position;
        	Vector2 parent_position = transform.parent.transform.position;

        	if(parent_position.x < hit_position.x){
        		direction = 1;
        	}
        	else if(parent_position.x > hit_position.x){
        		direction = -1;
        	}
        	else{
        		direction = 0;
        	}

        	target_position = Vector2.zero + hit_position;
        	target_position.x = hit_position.x + (direction * distance);
        }
	}
}
