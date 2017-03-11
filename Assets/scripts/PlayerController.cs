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
			var gb1 = Instantiate(unit, Spawners[selectedSpawner].transform.position + Vector3.up*2, Quaternion.identity);
			gb1.GetComponent<Renderer> ().material.color = Color.yellow;
			if (transform.position.x < 0)
				gb1.GetComponent<MoveUnitForward> ().turn = true;
		}
		if(KeyPress[(int)Buttons.Spawn2] && !DeltaPress[(int)Buttons.Spawn2]){
			var gb2 = Instantiate(unit, Spawners[selectedSpawner].transform.position + Vector3.up*2, Quaternion.identity);
			gb2.GetComponent<Renderer> ().material.color = Color.blue;
			if (transform.position.x < 0)
				gb2.GetComponent<MoveUnitForward> ().turn = true;
		}
		if(KeyPress[(int)Buttons.Spawn3] && !DeltaPress[(int)Buttons.Spawn3]){
			var gb3 = Instantiate(unit, Spawners[selectedSpawner].transform.position + Vector3.up*2, Quaternion.identity);
					gb3.GetComponent<Renderer> ().material.color = Color.cyan;
			if (transform.position.x < 0)
				gb3.GetComponent<MoveUnitForward> ().turn = true;
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