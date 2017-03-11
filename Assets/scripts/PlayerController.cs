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

	public string[] inputkeys = new string[5] ;
	public GameObject unit;
	List<GameObject> Spawners = new List<GameObject>();
	int selectedSpawner = 0;
	int deltaSelectedSpawner = 0;

	void Start () {
		foreach (Transform child in transform)
		{
			if (child.tag == "Spawner")
			{
				child.GetComponent<Renderer> ().material.color = Color.red;
				Spawners.Add(child.gameObject);
			}
		}

		Spawners[selectedSpawner].GetComponent<Renderer> ().material.color = Color.green;
	}
	bool[] KeyPress = new bool[5];
	bool[] DeltaPress = new bool[5];
	void Update () {
		foreach (Buttons btn in Enum.GetValues(typeof(Buttons))){
			KeyPress[(int)btn] = Input.GetKey(inputkeys[(int)btn]);
		}

		if(KeyPress[(int)Buttons.Spawn1] && !DeltaPress[(int)Buttons.Spawn1]){
			Vector3 spawnPoint = new Vector3 (
				Spawners[selectedSpawner].transform.position.x + (transform.position.x < 0 ?1:-1),
				0.05f,
				Spawners[selectedSpawner].transform.position.z);
			Quaternion spawnRotation = Quaternion.Euler(new Vector3(0.02f, (transform.position.x < 0 ?90:-90), 0));
			Instantiate(unit, spawnPoint, spawnRotation);
		}
		if(KeyPress[(int)Buttons.Spawn2] && !DeltaPress[(int)Buttons.Spawn2]){
			Vector3 spawnPoint = new Vector3 (
				Spawners[selectedSpawner].transform.position.x + (transform.position.x < 0 ?1:-1),
				0f,
				Spawners[selectedSpawner].transform.position.z);
			Quaternion spawnRotation = Quaternion.Euler(new Vector3(0.02f, (transform.position.x < 0 ?90:-90), 0));
			Instantiate(unit, spawnPoint, spawnRotation);
		}
		if(KeyPress[(int)Buttons.Spawn3] && !DeltaPress[(int)Buttons.Spawn3]){
			Vector3 spawnPoint = new Vector3 (
				Spawners[selectedSpawner].transform.position.x + (transform.position.x < 0 ?1:-1),
				0.05f,
				Spawners[selectedSpawner].transform.position.z);
			Quaternion spawnRotation = Quaternion.Euler(new Vector3(0.02f, (transform.position.x < 0 ?90:-90), 0));
			Instantiate(unit, spawnPoint, spawnRotation);
		}
		if (KeyPress[(int)Buttons.Up] && !DeltaPress[(int)Buttons.Up]) {
			if (selectedSpawner < Spawners.Count-1 ) {
				selectedSpawner++;
			}
		}
		else if(KeyPress[(int)Buttons.Down] && !DeltaPress[(int)Buttons.Down]){
			if (selectedSpawner > 0 ) {
				selectedSpawner--;
			}
		}
		if (selectedSpawner != deltaSelectedSpawner) {
			Spawners[deltaSelectedSpawner].GetComponent<Renderer> ().material.color = Color.red;
			Spawners[selectedSpawner].GetComponent<Renderer> ().material.color = Color.green;
		}
		deltaSelectedSpawner = selectedSpawner;
		foreach (Buttons btn in Enum.GetValues(typeof(Buttons))){
			DeltaPress[(int)btn] = KeyPress[(int)btn];
		}
	}
}