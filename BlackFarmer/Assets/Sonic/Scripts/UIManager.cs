using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private Text scores_txt;
	private Text time_txt;
	private Text speed_txt;
	private GameObject cart;

	private float timeElapsed=0;
	private float score=0;


	// Use this for initialization
	void Start () {
		scores_txt = GameObject.Find("/CanvasHUD/Stats/Player Score").GetComponent<Text>();
		speed_txt = GameObject.Find("/CanvasHUD/Stats/Speed").GetComponent<Text>();
		time_txt = GameObject.Find("/CanvasHUD/Stats/Time").GetComponent<Text>();
		cart = GameObject.Find("cart");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timeElapsed += Time.deltaTime;
		string time_string = "Time\n" + Mathf.Round(timeElapsed);
		time_txt.text = time_string;

		float cartVelocity = cart.GetComponent<Rigidbody2D>().velocity.magnitude;
		cartVelocity = Mathf.Round(cartVelocity);
		string speed_string = "Speed\n" + cartVelocity;
		speed_txt.text = speed_string;

		if(cartVelocity>0 && cartVelocity<=33){
			speed_txt.color = Color.blue;
		}
		if(cartVelocity>33 && cartVelocity<=66){
			speed_txt.color = Color.yellow;
		}
		if(cartVelocity>66 && cartVelocity<=100){
			speed_txt.color = Color.red;
		}
	}

	public void UpdateScore (float add_score) {
		score += add_score;
		string score_string = "Score\n" + score;
		scores_txt.text = score_string;
	}
}
