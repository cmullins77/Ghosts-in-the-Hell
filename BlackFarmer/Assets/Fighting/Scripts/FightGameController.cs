using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightGameController : MonoBehaviour {

    public List<PunchCombo> combos = new List<PunchCombo>();

    public GameObject img;
    public bool active;
    public int timer;

    public bool playing;

    public MicInput input;

    public Sprite[] spriteList;
    public Image background;

    public int score;
    public GameController gc;

    // Use this for initialization
    void Start () {
        PunchCombo newCombo = new PunchCombo();
        int numCombo = (int)Random.Range(3f, 12f);
        List<int> comboData = new List<int>();
        for (int i = 0; i < numCombo; i++) {
            comboData.Add(Random.Range(10, 100));
        }
        newCombo.comboData = comboData;
        newCombo.img = img;
        newCombo.input = input;
        newCombo.background = background;
        newCombo.spriteList = spriteList;
        newCombo.timer = newCombo.comboData[0];
        combos.Add(newCombo);

        gc = (GameController)FindObjectOfType(typeof(GameController));

        playing = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (playing) {
            if (combos[0].PunchUpdate()) {
                bool punch = combos[0].CheckPunch();
                print(punch);
                if (punch) {
                    background.sprite = spriteList[4];
                } else {
                    background.sprite = spriteList[3];
                }
                playing = false;
            }
            timer = combos[0].timer;
            active = combos[0].active;
        } else {
            gc.currentScore += combos[0].Score;
            StartCoroutine(goToNextGame());
        }
	}

    IEnumerator goToNextGame() {
        yield return new WaitForSeconds(2);
        gc.goToNext();
    }

}
