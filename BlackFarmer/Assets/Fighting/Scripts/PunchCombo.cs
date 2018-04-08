using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunchCombo : MonoBehaviour {

    public List<int> comboData;
    public GameObject img;
    public bool active;
    public int currNum;

    public int timer;
    public int redPunchTimer;

    public bool hit;
    public bool failed;

    public MicInput input;

    public GameObject canvas;

    public Sprite[] spriteList;
    public Image background;

    int numFailed;
    int numSuceeded;

    public int Score;

    // Use this for initialization
    void Start () {
        hit = false;
        failed = false;
	}
	
	// Update is called once per frame
	public bool PunchUpdate () {
       if (active) {
            if (redPunchTimer > 0) {
                redPunchTimer--;
                if (redPunchTimer == 0) {
                    background.sprite = spriteList[0];
                }
            }
            if (!hit && input.MicLoudness >= 0.1) {
                hit = true;
                print("HIT");
                background.sprite = spriteList[1];
                redPunchTimer = 0;
                numSuceeded++;
                Score += 300;
            }
            if (timer == 0) {
                active = false;
                timer = comboData[currNum];
                currNum++;
                img.SetActive(false);
                background.sprite = spriteList[0];
                if (!hit) {
                    numFailed++;
                    failed = true;
                    print(failed);
                    background.sprite = spriteList[2];
                    redPunchTimer = 20;
                }
                if (currNum == comboData.Count) {
                    return true;
                }
                hit = false;
            }
        } else {
            if (!hit && input.MicLoudness <= 0.01) {
                hit = true;
                print("QUIET");
                numSuceeded++;
                Score += 300;
            }
            if (timer == 0) {
                active = true;
                img.SetActive(true);
                background.sprite = spriteList[0];
                timer = 100;
                if (!hit) {
                    failed = true;
                    print("FAILED");
                    numFailed++;
                    background.sprite = spriteList[2];
                    redPunchTimer = 20;
                }
                hit = false;
            }
        }
        timer--;
        return false;
    }

    public bool CheckPunch() {
        return numSuceeded > numFailed*3;
    }

  
}
