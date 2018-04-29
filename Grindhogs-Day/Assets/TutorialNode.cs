using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNode : MonoBehaviour {
    public Color deactiveColor;
    public Color activeColor;
    public GameObject tutorialText;
    public GameObject player;

    public float x;
    public float y;
	// Use this for initialization
	void Start () {
        x = transform.position.x;
        y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        float pX = player.transform.position.x;
        float pY = player.transform.position.y;
        if (pX <= x + 0.4 && pX >= x - 0.4 && pY <= 0.8 + y && pY >= y - 0.8) {
            tutorialText.SetActive(true);
            GetComponent<SpriteRenderer>().color = activeColor;
        } else {
            tutorialText.SetActive(false);
            GetComponent<SpriteRenderer>().color = deactiveColor;
        }
	}
}
