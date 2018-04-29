using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLever : MonoBehaviour {
    public GameObject portal1;
    public GameObject portal2;

    public Sprite leverUp;
    public Sprite leverDown;

    public GameObject player;
    public bool on;


    private bool clickable;
    // Use this for initialization
    void Start () {
        GetComponent<SpriteRenderer>().sprite = leverUp;
        clickable = false;
        portal1.SetActive(false);
        portal2.SetActive(false);
        on = false;
    }
	
    private void OnMouseEnter() {
        clickable = true;
    }

    public void activateLever() {
        if (!on) {
            portal1.SetActive(true);
            portal2.SetActive(true);
            GetComponent<SpriteRenderer>().sprite = leverDown;
            on = true;
        }
    }

    public bool switchable() {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float leverX = transform.position.x;
        float leverY = transform.position.y;
        if (playerX >= leverX - 0.3 && playerX <= leverX + 0.3 && playerY >= leverY - 0.3 && playerY <= leverY + 0.3) {
            return true;
        }
        return false;
    }
}
