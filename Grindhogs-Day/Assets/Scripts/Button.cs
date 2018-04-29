using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    public GameObject[] characters;
    public Sprite pressedSprite;
    public Sprite notPressedSprite;
    public bool pressed;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        characters = GameObject.FindGameObjectsWithTag("Knight");
        bool foundPress = false;
	    for (int i = 0; i < characters.Length; i++) {
            GameObject character = characters[i];
            if (character.transform.position.y >= transform.position.y + 0.1 && character.transform.position.y <= transform.position.y + 0.2) {
                if (character.transform.position.x >= transform.position.x - 0.15 && character.transform.position.x <= transform.position.x + 0.15) {
                    foundPress = true;
                }
            }
        }	
        if (foundPress) {
            pressed = true;
            GetComponent<SpriteRenderer>().sprite = pressedSprite;
        } else {
            pressed = false;
            GetComponent<SpriteRenderer>().sprite = notPressedSprite;
        }
	}
}
