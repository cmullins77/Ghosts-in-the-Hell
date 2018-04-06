using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingGame : MonoBehaviour{

    public RectTransform okayBar;
    public float okayMid;
    public RectTransform goodBar;
    public float goodMid;
    public RectTransform timeBar;

    public RectTransform flames;

    public int cookedLevelGoodLow;
    public int cookedLevelGoodHigh;
    public int cookedLevelOkayLow;
    public int cookedLevelOkayHigh;

    public Burger burger;
    public Burger[] burgers;
    public int burgerNum;
    public GameObject arrow;

    public int cookingFactor;

    public int time;
    public RectTransform clockHand;

    public bool playing;

    public int gameLength;
    public int barLength;
    public float timeSpeed;

    // Use this for initialization
    void Start () {
        play();
        gameLength = 1800;
        timeSpeed = barLength / (gameLength*1.0f);
    }

    public float getTimeSpeed() {
        return timeSpeed;
    }

    public void setup() {
        burgerNum = 0;
        nextBurger();
    }

    public void nextBurger() {
        burger = burgers[burgerNum];
        burger.makeActive();
        time = 360;
        cookedLevelGoodHigh = burger.cookedLevelGoodHigh;
        cookedLevelGoodLow = burger.cookedLevelGoodLow;
        cookedLevelOkayHigh = burger.cookedLevelOkayHigh;
        cookedLevelOkayLow = burger.cookedLevelOkayLow;

        okayMid = (cookedLevelOkayHigh - cookedLevelOkayLow) / 2.0f + cookedLevelOkayLow - 50;
        okayBar.sizeDelta = new Vector2(cookedLevelOkayHigh - cookedLevelOkayLow, 20);
        okayBar.transform.localPosition = new Vector3(okayMid, okayBar.transform.localPosition.y, 1);
        goodMid = (cookedLevelGoodHigh - cookedLevelGoodLow) / 2.0f + cookedLevelGoodLow - 50;
        goodBar.sizeDelta = new Vector2(cookedLevelGoodHigh - cookedLevelGoodLow, 20);
        goodBar.transform.localPosition = new Vector3(goodMid, goodBar.transform.localPosition.y, 1);

        playing = true;
    }

    public void play() {
        setup();
    }
    public void stopGame() {
        playing = false;
    }

    // Update is called once per frame
    void Update() {
        if (playing) {
            if (burger.cookedLevel < 100) {
                if (Input.GetKey("1")) {
                    cookingFactor = 10;
                    burger.cookedLevel = burger.cookedLevel + 0.1f;
                }
                else if (Input.GetKey("2")) {
                    cookingFactor = 20;
                    burger.cookedLevel = burger.cookedLevel + 0.2f;
                }
                else if (Input.GetKey("3")) {
                    cookingFactor = 30;
                    burger.cookedLevel = burger.cookedLevel + 0.3f;
                }
                else if (Input.GetKey("4")) {
                    cookingFactor = 40;
                    burger.cookedLevel = burger.cookedLevel + 0.4f;
                }
                else if (Input.GetKey("5")) {
                    cookingFactor = 50;
                    burger.cookedLevel = burger.cookedLevel + 0.5f;
                }
                else if (Input.GetKey("6")) {
                    cookingFactor = 60;
                    burger.cookedLevel = burger.cookedLevel + 0.6f;
                }
                else if (Input.GetKey("7")) {
                    cookingFactor = 70;
                    burger.cookedLevel = burger.cookedLevel + 0.7f;
                }
                else if (Input.GetKey("8")) {
                    cookingFactor = 80;
                    burger.cookedLevel = burger.cookedLevel + 0.8f;
                }
                else if (Input.GetKey("9")) {
                    cookingFactor = 90;
                    burger.cookedLevel = burger.cookedLevel + 0.9f;
                }
                else if (Input.GetKey("0")) {
                    cookingFactor = 100;
                    burger.cookedLevel = burger.cookedLevel + 1.0f;
                }
                else {
                    cookingFactor = 0;
                }

            }
            else {
                cookingFactor = 0;
            }
            time--;
            clockHand.Rotate(new Vector3(0, 0, -1));
            flames.sizeDelta = new Vector2(flames.sizeDelta.x, 60.0f * (cookingFactor / 100.0f));
            timeBar.sizeDelta = new Vector2(timeBar.sizeDelta.x - timeSpeed, timeBar.sizeDelta.y);
            arrow.transform.localPosition = new Vector3(burger.cookedLevel - 50, arrow.transform.localPosition.y, 1);
            if (time == 0) {
                burgerNum++;
                if (burgerNum == 5) {
                    playing = false;
                    //show score
                    //load next game
                } else {
                    nextBurger();
                }
            }
        }
    }
}
