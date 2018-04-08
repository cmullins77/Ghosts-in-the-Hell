using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour {
    public GameController gc;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI yourScoreText;
    // Use this for initialization
    void Start () {
        gc = (GameController)FindObjectOfType(typeof(GameController));
        if (gc.currentScore > gc.highScore) {
            gc.highScore = gc.currentScore;
        }
        highScoreText.text = gc.highScore.ToString();
        yourScoreText.text = gc.currentScore.ToString();
    }
}
