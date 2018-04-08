using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingGame : MonoBehaviour{

    public MicInput input;

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

    public float cookingFactor;

    public int time;
    public RectTransform clockHand;

    public bool playing;

    public int gameLength;
    public int barLength;
    public float timeSpeed;

    public int score;
    public GameController gc;


    public Sprite[] burgerSprites;
    public Image[] burgerImages;
    public GameObject[] burgerImageObjs;

    public string[] patties;

    // Use this for initialization
    void Start () {
        play();
        gameLength = 900;
        timeSpeed = barLength / (gameLength*1.0f);
        gc = (GameController)FindObjectOfType(typeof(GameController));
    }

    public float getTimeSpeed() {
        return timeSpeed;
    }

    public void setup() {
        burgerNum = 0;
        nextBurger();
    }

    public void nextBurger() {
        burger.makeInactive();
        burger = burgers[burgerNum];
        burger.makeActive();
        time = 180;
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
        burger.GetComponent<Animator>().Play(patties[burger.getDoneness()]);
        if (playing) {
            if (burger.cookedLevel < 100) {
                float val = input.MicLoudness;
                cookingFactor = val;
                burger.cookedLevel = burger.cookedLevel + val;
            }
            else {
                cookingFactor = 0;
            }
            time--;
            clockHand.Rotate(new Vector3(0, 0, -2));
            flames.sizeDelta = new Vector2(flames.sizeDelta.x, 60.0f * cookingFactor);
            timeBar.sizeDelta = new Vector2(timeBar.sizeDelta.x - timeSpeed, timeBar.sizeDelta.y);
            arrow.transform.localPosition = new Vector3(burger.cookedLevel - 50, arrow.transform.localPosition.y, 1);
            if (time == 0) {
                burgerImages[burgerNum].sprite = burgerSprites[burger.getDoneness()];
                burgerImageObjs[burgerNum].SetActive(true);
                burgerNum++;
                if (burgerNum == 5) {
                    burger.makeInactive();
                    playing = false;
                    score = burgers[0].getScore() + burgers[1].getScore() + burgers[2].getScore() + burgers[3].getScore() + burgers[4].getScore();
                    gc.currentScore += score;
                    StartCoroutine(goToNextGame());
                    //show score
                    //load next game
                } else {
                    nextBurger();
                }
            }
        }
    }
    IEnumerator goToNextGame() {
        yield return new WaitForSeconds(2);
        gc.goToNext();
    }
}
