using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarUseController : MonoBehaviour {

	int layerInd = -1;
	AvatarController ac;

	// Use this for initialization
	void Start () {
		ac = transform.parent.gameObject.GetComponent<AvatarController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Object"){
        	ac.isCarrying = true;
        	other.transform.parent = transform.parent; //make object a child of the player
        	print(other.transform.parent.gameObject.name);
        }
    }
}
