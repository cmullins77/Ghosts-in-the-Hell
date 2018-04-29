using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public bool on;
    public int startTime;

    public bool movingUp;
    public float bottomPosition;
    public float topposition;
    public float startPosition;

    public int pauseCount;
    public SpawnManager sM;

	// Use this for initialization
	void Start () {
        startPosition = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (on && startTime <= sM.totalTimer) {
            if (pauseCount > 0) {
                pauseCount--;
            } else if (movingUp) {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.005f, transform.position.z);
                if (transform.position.y >= topposition) {
                    transform.position = new Vector3(transform.position.x, topposition, transform.position.z);
                    movingUp = false;
                    pauseCount = 30;
                }
            } else {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.005f, transform.position.z);
                if (transform.position.y <= bottomPosition) {
                    transform.position = new Vector3(transform.position.x, bottomPosition, transform.position.z);
                    movingUp = true;
                    pauseCount = 30;
                }
            }
        } else if (startTime > sM.totalTimer) {
            transform.position = new Vector3(transform.position.x, startPosition, transform.position.z);
        }
	}

    public void turnOn() {
        on = true;
        startTime = 100;
        if (transform.position.y == bottomPosition) {
            movingUp = true;
        }
    }


}
