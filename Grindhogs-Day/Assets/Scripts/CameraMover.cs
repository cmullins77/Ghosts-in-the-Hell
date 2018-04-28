using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {
    public GameObject camera;
    public Animator camAnimator;
    public GameObject player;
    public int roomNum;
    private void Start() {
        camAnimator = camera.GetComponent<Animator>();
        camAnimator.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		if (roomNum == 1) {
            if (player.transform.position.x > 1.78) {
                //camera.transform.position = new Vector3(3.48f, 0, -10);
                camAnimator.enabled = true;
                camAnimator.Play("Room1to2");
                roomNum = 2;
            }
        } else if (roomNum == 2) {
            if (player.transform.position.x < 1.78) {
                //camera.transform.position = new Vector3(0, 0, -10);
                camAnimator.enabled = true;
                camAnimator.Play("Room2to1");
                roomNum = 1;
            }
        }
	}
}
