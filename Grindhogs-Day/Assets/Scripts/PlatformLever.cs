using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLever : MonoBehaviour {

    public GameObject lever;
    public MovingPlatform platform;
    public Sprite leverUp;
    public Sprite leverDown;

    public GameObject player;

    public bool locked;

    private bool clickable;


    // Use this for initialization
    void Start() {
        lever.GetComponent<SpriteRenderer>().sprite = leverUp;
        clickable = false;
    }

    // Update is called once per frame
    void Update() {
        // if (!gateMoving && switchable()) {
        //     if (clickable && Input.GetMouseButtonDown(1)) {
        //         activateLever();
        //     }
        // } else {


        // }
    }

    private void OnMouseEnter() {
        clickable = true;
    }

    public void activateLever() {
        if (!platform.on && !locked) {
            platform.turnOn();
            lever.GetComponent<SpriteRenderer>().sprite = leverDown;
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
}
