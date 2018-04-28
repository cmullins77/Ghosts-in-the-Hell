using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLever : MonoBehaviour {

    public GameObject lever;
    public GameObject gate;
    public int gateHeight;
    public Sprite leverUp;
    public Sprite leverDown;

    public bool on;
    public GameObject player;

    private bool clickable;
    private bool gateMoving;
    private int gateTime;

	// Use this for initialization
	void Start () {
        lever.GetComponent<SpriteRenderer>().sprite = leverUp;
        clickable = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!gateMoving && switchable()) {
            if (clickable && Input.GetMouseButtonDown(1)) {
                activateLever();
            }
        } else {
            if (gateTime > 0) {
                animateGate(0.005f);
                gateTime--;
            } else {
                animateGate(-0.005f);
                gateTime++;
            }
            if (gateTime == 0) {
                gateMoving = false;
            }

        }
	}

    private void OnMouseEnter() {
        clickable = true;
    }

    public void activateLever() {
        if (on) {
            on = false;
            lever.GetComponent<SpriteRenderer>().sprite = leverUp;
            gateMoving = true;
            gateTime = gateHeight * -60;
        } else {
            on = true;
            lever.GetComponent<SpriteRenderer>().sprite = leverDown;
            gateMoving = true;
            gateTime = gateHeight * 60;
        }
    }

    public bool switchable() {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float leverX = lever.transform.position.x;
        float leverY = lever.transform.position.y;
        if (playerX >= leverX - 0.3 && playerX <= leverX + 0.3 && playerY >= leverY - 0.3 && playerY <= leverY + 0.3) {
            return true;
        }
        return false;
    }

    public void animateGate(float i) {
        gate.transform.position = new Vector3(gate.transform.position.x, gate.transform.position.y + i, gate.transform.position.z);
    }
}
