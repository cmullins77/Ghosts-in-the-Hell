using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicPuzzle : MonoBehaviour {
    public GateLever lever1;
    public GateLever lever2;

    public Button[] firstButtons;
    public Button[] secondButtons;
    public CameraMover cam;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
        check1();
        check2();
	}

    public void check1() {
        if ((firstButtons[0].pressed || firstButtons[1].pressed) && (firstButtons[2].pressed || firstButtons[3].pressed)) {
            lever1.turnOn();
        }
    }
    public void check2() {
        if ((secondButtons[0].pressed && secondButtons[1].pressed) && (secondButtons[2].pressed || secondButtons[3].pressed) && (!secondButtons[4].pressed && secondButtons[5].pressed)) {
            lever2.turnOn();
        }
    }
}
