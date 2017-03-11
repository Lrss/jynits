using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
	int playernr;

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
		MySpawners[selectedSpawner].GetComponent<Renderer> ().material.color = (playernr == 1? Color.blue :Color.red);
	}

	void spawn(GameObject unitPrefab){
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
	}

	bool[] KeyPress = new bool[5];
	bool[] DeltaPress = new bool[5];
	void Update () {
		//Button update.
		foreach (Buttons btn in Enum.GetValues(typeof(Buttons))){
			KeyPress[(int)btn] = Input.GetKey(inputkeys[(int)btn]);
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
			if (selectedSpawner < MySpawners.Count-1 ) {
				selectedSpawner++;
				updateSpawnerColor();
			}
		}
		else if(KeyPress[(int)Buttons.Down] && !DeltaPress[(int)Buttons.Down]){
			if (selectedSpawner > 0 ) {
				selectedSpawner--;
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
		MySpawners[deltaSelectedSpawner].GetComponent<Renderer> ().material.color = Color.black;
		MySpawners[selectedSpawner].GetComponent<Renderer> ().material.color = (playernr == 1? Color.blue :Color.red);
	}
}