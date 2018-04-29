using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCamera : MonoBehaviour {
    public GameObject player;
    public float max;
    public float min;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float playerPos = player.transform.position.x;
        if (playerPos < min) {
            transform.position = new Vector3(min, 0, -10);
        } else if (playerPos > max) {
            transform.position = new Vector3(max, 0, -10);
        } else {
            transform.position = new Vector3(playerPos, 0, -10);
        }
        if (playerPos > 11.7) {
            SceneManager.LoadScene(0);
        }
	}
}
