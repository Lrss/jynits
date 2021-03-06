﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	enum Buttons
	{
		Spawn1,
		Spawn2,
		Spawn3,
		Up,
		Down
	}

	GameObject enemy; 
	public string[] inputkeys = new string[5] ;

	GameObject unitPrefab01;
	GameObject unitPrefab02;
	GameObject unitPrefab03;

	public List<GameObject> EnemySpawners = new List<GameObject>();
	public List<GameObject> MySpawners = new List<GameObject>();
	int selectedSpawner = 0;
	int deltaSelectedSpawner = 0;
	public int playernr;
	float startTime;
	void Start () {
		if (name == "Player01") {
			playernr = 1;
		} else {
			playernr = 2;
		}
		enemy = GameObject.Find ("Player0"+(playernr == 1 ? 2 : 1));
		unitPrefab01 = Resources.Load ("prefabs/" +(playernr == 1? "blue":"red")+ "_swordJynit1") as GameObject;
		unitPrefab02 = Resources.Load ("prefabs/" +(playernr == 1? "blue":"red")+ "_swordJynit2") as GameObject;
		unitPrefab03 = Resources.Load ("prefabs/" +(playernr == 1? "blue":"red")+ "_swordJynit3") as GameObject;

		tag = playernr.ToString();
		foreach (Transform child in transform)
		{
			if (child.tag == "Spawner")
				child.tag = playernr.ToString();
			if (child.tag == "1" || child.tag == "2")
			{
				child.GetComponent<Renderer> ().material.color = Color.black;
				MySpawners.Add(child.gameObject);
			}
		}
		foreach (Transform child in enemy.transform)
		{
			if (child.tag == "1" || child.tag == "2" || child.tag == "Spawner")
			{
				EnemySpawners.Add(child.gameObject);
			}
		}
		selectedSpawner = MySpawners.Count / 2;
		MySpawners [selectedSpawner].GetComponent<Renderer> ().enabled = true;
		startTime = Time.time;
	}

	void spawn(GameObject unitPrefab){
		if (jynits.Count >= maxUnits)
			return;
		Vector3 spawnPoint = new Vector3 (
			MySpawners[selectedSpawner].transform.position.x ,//+ (transform.position.x < 0 ?1:-1),
			0.05f,
			MySpawners[selectedSpawner].transform.position.z);
		Quaternion spawnRotation = Quaternion.Euler(new Vector3(0, (transform.position.x < 0 ?90:-90), 0));
		GameObject jynit;
		jynit = Instantiate (unitPrefab, spawnPoint, spawnRotation);
		jynit.gameObject.tag = (transform.position.x < 0 ? "1" : "2");
		jynit.AddComponent<Rigidbody> ();
		var att = jynit.AddComponent<Attack> ();
		att.mySpawner = MySpawners [selectedSpawner];
		att.enemySpawner = EnemySpawners [selectedSpawner];
        Fabric.EventManager.Instance.PostEvent("UnitSpawn", gameObject);
		jynits.Add (jynit);
	}

	List<GameObject> jynits = new List<GameObject>();

	int gameTime;
	int maxUnits;
	public Text PlayerUpkeep;

	bool[] KeyPress = new bool[5];
	bool[] DeltaPress = new bool[5];
	void Update () {
		if (ScoreManager.player01Score > 1 || ScoreManager.player02Score > 1)
		return;

		//Regainable units
		//jynits.RemoveAll(item => item == null);
		gameTime = (int)(Time.time - startTime);
		maxUnits = (gameTime / 3) + 2;
		PlayerUpkeep.text = "Player "+playernr+"\n" + jynits.Count + "/" + maxUnits;
		//Button update.
		foreach (Buttons btn in Enum.GetValues(typeof(Buttons))){
			KeyPress[(int)btn] = Input.GetKey(inputkeys[(int)btn]);
		}

		if (MySpawners [selectedSpawner] == null) {
			int temp = selectedSpawner;
			for (int i = 0; temp - i >= 0 || temp + i <= MySpawners.Count - 1; i++) {
				if (temp + i <= MySpawners.Count -1 && MySpawners [temp + i] != null) {
					selectedSpawner = temp + i;
					MySpawners [selectedSpawner].GetComponent<Renderer> ().enabled = true;
					break;
				}
				if (temp - i >= 0 && MySpawners [temp - i] != null) {
					selectedSpawner = temp - i;
					MySpawners [selectedSpawner].GetComponent<Renderer> ().enabled = true;
					break;
				}
			}
		}

		if(KeyPress[(int)Buttons.Spawn1] && !DeltaPress[(int)Buttons.Spawn1]){
			spawn (unitPrefab01);
		}
		if(KeyPress[(int)Buttons.Spawn2] && !DeltaPress[(int)Buttons.Spawn2]){
			spawn (unitPrefab02);
		}
		if(KeyPress[(int)Buttons.Spawn3] && !DeltaPress[(int)Buttons.Spawn3]){
			spawn (unitPrefab03);
		}

		if (KeyPress[(int)Buttons.Up] && !DeltaPress[(int)Buttons.Up]) {
			int tempSelecter = selectedSpawner;
			do {
				tempSelecter++;
			} while (tempSelecter < MySpawners.Count - 1 && MySpawners [tempSelecter] == null);
			if (tempSelecter <= MySpawners.Count -1 ) {
				selectedSpawner = tempSelecter;
				updateSpawnerColor();
			}
		}
		else if(KeyPress[(int)Buttons.Down] && !DeltaPress[(int)Buttons.Down]){
			int tempSelecter = selectedSpawner;
			do {
				tempSelecter--;
			} while ( tempSelecter > 0 && MySpawners [tempSelecter] == null);
					
			if (tempSelecter >= 0 ) {
				selectedSpawner = tempSelecter;
				updateSpawnerColor();
			}
		}

		//delta update.
		deltaSelectedSpawner = selectedSpawner;
		foreach (Buttons btn in Enum.GetValues(typeof(Buttons))){
			DeltaPress[(int)btn] = KeyPress[(int)btn];
		}
	}
	void updateSpawnerColor(){
		if (MySpawners [deltaSelectedSpawner] != null) {
			MySpawners [deltaSelectedSpawner].GetComponent<Renderer> ().enabled = false;
		}
		MySpawners [selectedSpawner].GetComponent<Renderer> ().enabled = true;
	}
}