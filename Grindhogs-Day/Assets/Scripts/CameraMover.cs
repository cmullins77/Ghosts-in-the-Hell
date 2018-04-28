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
            } else if (player.transform.position.x > 5.25) {
                camAnimator.enabled = true;
                camAnimator.Play("Room2to3");
                roomNum = 3;
            }
        } else if (roomNum == 3) {
            if (player.transform.position.x < 5.25) {
                camAnimator.enabled = true;
                camAnimator.Play("Room3to2");
                roomNum = 2;
            } else if (player.transform.position.y > 1.05) {
                camAnimator.enabled = true;
                camAnimator.Play("Room3to4");
                roomNum = 4;
            }
        } else if (roomNum == 4) {
            if (player.transform.position.x < 5.25) {
                camAnimator.enabled = true;
                camAnimator.Play("Room4to5");
                roomNum = 5;
            } else if (player.transform.position.y < 1.05) {
                camAnimator.enabled = true;
                camAnimator.Play("Room4to3");
                roomNum = 3;
            } else if (player.transform.position.y > 3.06) {
                camAnimator.enabled = true;
                camAnimator.Play("Room4to9");
                roomNum = 9;
            }
        } else if (roomNum == 5) {
            if (player.transform.position.x > 5.25) {
                camAnimator.enabled = true;
                camAnimator.Play("Room5to4");
                roomNum = 4;
            } else if (player.transform.position.y > 3.06) {
                camAnimator.enabled = true;
                camAnimator.Play("Room5to8");
                roomNum = 8;
            } else if (player.transform.position.x < 1.78) {
                camAnimator.enabled = true;
                camAnimator.Play("Room5to6");
                roomNum = 6;
            }  
        } else if (roomNum == 6) {
            if (player.transform.position.x > 1.78) {
                camAnimator.enabled = true;
                camAnimator.Play("Room6to5");
                roomNum = 5;
            } else if (player.transform.position.y > 3.06) {
                camAnimator.enabled = true;
                camAnimator.Play("Room6to7");
                roomNum = 7;
            }

        } else if (roomNum == 7) {
            if (player.transform.position.x > 1.78) {
                camAnimator.enabled = true;
                camAnimator.Play("Room7to8");
                roomNum = 8;
            }
            else if (player.transform.position.y < 3.06) {
                camAnimator.enabled = true;
                camAnimator.Play("Room7to6");
                roomNum = 6;
            }

        } else if (roomNum == 8) {
            if (player.transform.position.y < 3.06) {
                camAnimator.enabled = true;
                camAnimator.Play("Room8to5");
                roomNum = 5;
            } else if (player.transform.position.x < 1.78) {
                camAnimator.enabled = true;
                camAnimator.Play("Room8to7");
                roomNum = 7;
            }

        } else if (roomNum == 9) {
            if (player.transform.position.y < 3.06) {
                camAnimator.enabled = true;
                camAnimator.Play("Room9to4");
                roomNum = 4;
            }
        }
    }
}
