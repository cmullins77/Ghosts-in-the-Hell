using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour {

    public float cookedLevel;
    public int cookedLevelGoodLow;
    public int cookedLevelGoodHigh;
    public int cookedLevelOkayLow;
    public int cookedLevelOkayHigh;

    public GameObject img;

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void makeActive() {
        img.SetActive(true);
    }
    public void makeInactive() {
        img.SetActive(false);
    }

    public int getScore() {
        if (getDoneness() == 2) {
            return 1000;
        } else if (getDoneness() == 1 || getDoneness() == 3) {
            return 500;
        } else {
            return 0;
        }
    }
    public int getDoneness() {
        if (cookedLevel < cookedLevelOkayLow) {
            return 0;
        } else if (cookedLevel < cookedLevelGoodLow) {
            return 1;
        } else if (cookedLevel <= cookedLevelGoodHigh) {
            return 2;
        } else if (cookedLevel <= cookedLevelOkayHigh) {
            return 3;
        } else {
            return 4;
        }
    }
}
