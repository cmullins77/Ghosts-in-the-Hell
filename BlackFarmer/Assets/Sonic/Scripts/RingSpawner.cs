using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawner : MonoBehaviour {

	public GameObject ring;

	private float radius = 14f;
	private float centre_x = 0;
	private float centre_y = 0;

	private float spawn_interval = 5f;
	private float time_elapsed = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		time_elapsed += Time.deltaTime;

		if(time_elapsed > spawn_interval){
			float angle = 2 * Mathf.PI * Random.value;
			float x = Mathf.Cos(angle)*radius;
			float y = Mathf.Sin(angle)*radius;

			Vector2 ring_position = new Vector2(x,y);

			Instantiate(ring, ring_position, transform.rotation);

			time_elapsed = 0;
		}
	}
}
