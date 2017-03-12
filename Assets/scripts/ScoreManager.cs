using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
	public static int player01Score = 0;
	public static int player02Score = 0;

	public Text TowerScore;
	public Text Win;

	float startTime;

	void Start () {
		startTime = Time.time;
	}
	int wintime;
	int gameTime;
	// Update is called once per frame
	void Update () {
		TowerScore.text = player01Score + " - " + player02Score + "\n" + gameTime;
		if (player01Score > 1) {
			Win.text = "Player 1\nWON";
		} else if (player02Score > 1) {
			Win.text = "Player 2\nWON";
		} else {
			gameTime = (int)(Time.time - startTime);
			wintime = (int)(Time.time);
			return;
		}
		if (Time.time - wintime > 5) {
			player01Score = 0;
			player02Score = 0;
			Application.LoadLevel (0);
		}
	}
}
