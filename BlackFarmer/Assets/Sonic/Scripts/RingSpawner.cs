using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawner : MonoBehaviour {

	public GameObject ring_blue;
	public GameObject ring_yellow;
	public GameObject ring_red;
	
	public float ring_limit = 10;
	public static float ring_count = 0;

	private float radius = 14f;
	private float centre_x = 0;
	private float centre_y = 0;

	private float spawn_interval = 1f;
	private float time_elapsed = 0;
	private bool flag = true;
	private GameObject uim;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!flag && ring_count<=0){
			flag = true;
		}

		if(flag && ring_count < ring_limit){
			time_elapsed += Time.deltaTime;
			if(time_elapsed > spawn_interval){
				int ring_id = Random.Range(0,3);
				SpawnRing(ring_id);
				time_elapsed = 0;
				if(ring_count>=10)
					flag=false;
			}
		}
	}

	void SpawnRing (int ring_id) {
		GameObject ring = ring_blue;
		if(ring_id==0)
			ring = ring_blue;
		if(ring_id==1)
			ring = ring_yellow;
		if(ring_id==2)
			ring = ring_red;
		float angle = 2 * Mathf.PI * Random.value;
		float x = Mathf.Cos(angle)*radius;
		float y = Mathf.Sin(angle)*radius;

		Vector2 ring_position = new Vector2(x,y);

		Instantiate(ring, ring_position, transform.rotation);
		ring_count += 1;
	}

	public void KillPing () {
		uim = GameObject.Find("GUIManager");
		uim.GetComponent<UIManager>().UpdateScore(100);
	}

}
