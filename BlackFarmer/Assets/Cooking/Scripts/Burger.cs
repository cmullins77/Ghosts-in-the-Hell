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
}
