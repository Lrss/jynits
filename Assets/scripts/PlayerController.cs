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

	public string[] inputkeys = new string[Enum.GetNames(typeof(MyEnum)).Length] ;
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
	bool[] KeyPress = new bool[Enum.GetNames(typeof(MyEnum)).Length];
	bool[] DeltaPress = new bool[Enum.GetNames(typeof(MyEnum)).Length];
	void Update () {
		foreach (Buttons btn in Enum.GetValues(typeof(Buttons))){
			KeyPress[btn] = Input.GetKey(inputkeys[btn]);
		}

		if(KeyPress[Buttons.Spawn1] && !DeltaPress[Buttons.Spawn1]){
			var gb = Instantiate(unit, Spawners[selectedSpawner].transform.position + Vector3.up*2, Quaternion.identity);
			if (transform.position.x < 0) {
				gb.GetComponent<Renderer> ().material.color = Color.yellow;
				gb.GetComponent<MoveUnitForward> ().turn = true;
			} else {
				gb.GetComponent<Renderer> ().material.color = Color.blue;
			}
		}

		if (KeyPress[Buttons.Up] && !DeltaPress[Buttons.Up]) {
			if (selectedSpawner < Spawners.Count-1 ) {
				selectedSpawner++;
			}
		}
		else if(KeyPress[Buttons.Down] && !DeltaPress[Buttons.Down]){
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
				DeltaPress[btn] = KeyPress[btn];
		}
	}
}