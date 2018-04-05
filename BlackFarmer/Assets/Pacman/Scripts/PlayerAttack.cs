using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public GameObject soundwave_right;
	public GameObject soundwave_left;
	public GameObject soundwave_up;
	public GameObject soundwave_down;

	private Transform playerT;
	private float rxnTime = 0;
	private float force = 0;
	private MicInput mi;

	// Use this for initialization
	void Start () {

		playerT = GetComponent<Transform>();
		mi = GetComponent<MicInput>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rxnTime = rxnTime + Time.deltaTime;
		force = mi.MicLoudness * 1000;

		if(rxnTime > 0.3){
			if (force > 1){		
				print("Force: "+force);

				GameObject sw_r = Instantiate(soundwave_right, playerT.position, playerT.rotation);
				GameObject sw_l = Instantiate(soundwave_left, playerT.position, playerT.rotation);
				GameObject sw_u = Instantiate(soundwave_up, playerT.position, playerT.rotation);
				GameObject sw_d = Instantiate(soundwave_down, playerT.position, playerT.rotation);
				
				float dtl = 2f; //distance to live

				if(force > 5)
					dtl = 10f;
				if(force > 10)
					dtl = 20f;

				sw_r.GetComponent<Soundwave>().distanceToLive = dtl;
				sw_l.GetComponent<Soundwave>().distanceToLive = dtl;
				sw_u.GetComponent<Soundwave>().distanceToLive = dtl;
				sw_d.GetComponent<Soundwave>().distanceToLive = dtl;

				rxnTime = 0;
			}
		}
	}
}
