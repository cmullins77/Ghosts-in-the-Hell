using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseController : MonoBehaviour {

	int layerInd = -1;
	PlayerController pc;

	// Use this for initialization
	void Start () {
		pc = transform.parent.gameObject.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Object"){
        	pc.isCarrying = true;
        	other.transform.parent = transform.parent; //make object a child of the player
        }
        if (other.gameObject.name == "Lever") {
            GateLever gl = other.gameObject.GetComponent<GateLever>();
            gl.activateLever();
        }
        if (other.gameObject.name == "Platform Lever") {
            PlatformLever pl = other.gameObject.GetComponent<PlatformLever>();
            pl.activateLever();
        }
        if (other.gameObject.name == "PortalLever") {
            PortalLever pl = other.gameObject.GetComponent<PortalLever>();
            pl.activateLever();
        }
    }
}
