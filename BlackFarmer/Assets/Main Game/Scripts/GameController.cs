using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public string[] games;
    public int numLevels;
    public List<string> levels;

    public int currentLevel;
    public int currentScore;
    public int highScore;


    public MicInput input;

    public GameObject MainMenu;
    public GameObject EndMenu;

    public int timer;

    public GameObject dayText;
    public GameObject mainTitle;
    public GameObject mainInstructions;

    // Use this for initialization
    void Start() {
        restartGame();
        highScore = 0;
        DontDestroyOnLoad(gameObject);
    }

    public void restartGame() {
        levels = new List<string>();
        for (int i = 0; i < numLevels; i++) {
            float randNum = Random.Range(0, games.Length);
            levels.Add(games[(int)randNum]);
        }
        currentLevel = -1;
        currentScore = 0;
        timer = 20;
    }
	
    public void goToNext() {
        currentLevel++;
        if (currentLevel == numLevels) {
            dayText.GetComponent<TextMeshProUGUI>().text = "Thanks for playing!";
            SceneManager.LoadScene("End Game");
        } else {
            int day = currentLevel + 1;
            dayText.GetComponent<TextMeshProUGUI>().text = "Day: " + day;
            SceneManager.LoadScene(levels[currentLevel]);
        }
    }

	// Update is called once per frame
	void Update () {
        if (currentLevel == -1) {
            if (timer > 0) {
                timer--;
            }
            float val = input.MicLoudness;
            if (val > 0.3) {
                if (timer == 0) {
                    dayText.SetActive(true);
                    mainTitle.SetActive(false);
                    mainInstructions.SetActive(false);
                    goToNext();
                }
            }
        }
        if (currentLevel == numLevels) {
            float val = input.MicLoudness;
            if (val > 0.3) {
                restartGame();
                SceneManager.LoadScene("Main Menu");
                mainInstructions.SetActive(true);
                mainTitle.SetActive(true);
                dayText.SetActive(false);
            }
        }
	}
}
