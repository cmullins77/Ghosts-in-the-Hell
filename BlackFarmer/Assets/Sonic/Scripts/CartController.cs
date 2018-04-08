using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour {

	public float thrustMultiplier = 100;

	private Rigidbody2D rbody;
	private float micVol = 0;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		micVol = GetComponent<MicInput>().MicLoudness;
		// if(Input.GetKeyDown("space"))
		rbody.AddForce(transform.right * thrustMultiplier * micVol);
	}
}
