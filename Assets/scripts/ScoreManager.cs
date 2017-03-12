using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int Player01Score = 0;
	public int Player02Score = 0;
	public GameObject player01;
	public GameObject player02;


	public Text Player01Upkeep;
	public Text Player02Upkeep;
	public Text TowerScore;

	float startTime;
	PlayerController p1;
	PlayerController p2;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		p1 = GetComponent<PlayerController> ();
		p2 = GetComponent<PlayerController> ();
	}
	int gameTime;
	// Update is called once per frame
	void Update () {
		gameTime = (int)(Time.time - startTime);
		TowerScore.text = Player01Score + " - " + Player02Score + "\n" + gameTime;

		Player01Upkeep.text = "Player 1\n" + "0/0"; //p1 upkeep
		Player02Upkeep.text = "Player 2\n" + "0/0"; //p2 upkeep
	}
}
