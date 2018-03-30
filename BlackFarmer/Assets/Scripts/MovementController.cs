using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

	private Rigidbody rbody;
	private MicInput mInput;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody>();
		mInput = GetComponent<MicInput>();
	}
	
	// Update is called once per frame
	void Update () {
		float force = MicInput.MicLoudness;
		//ignore mic loudness less than 10E-5

		force = force * 100;
		print("the mic: "+force);

		rbody.AddForce(transform.up * force);	
	}
}
