using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public string[] inputkeys = new string[2] ;
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
	bool[] KeyPress = new bool[2];
	bool[] DeltaPress = new bool[2];
	void Update () {
		KeyPress[0] = Input.GetKey(inputkeys[0]);
		KeyPress[1] = Input.GetKey(inputkeys[1]);
		if (KeyPress[0] && !DeltaPress[0]) {
			if (selectedSpawner < Spawners.Count-1 ) {
				selectedSpawner++;
			}
		}
		else if(KeyPress[1] && !DeltaPress[1]){
			if (selectedSpawner > 0 ) {
				selectedSpawner--;
			}
		}
		if (selectedSpawner != deltaSelectedSpawner) {
			Spawners[deltaSelectedSpawner].GetComponent<Renderer> ().material.color = Color.red;
			Spawners[selectedSpawner].GetComponent<Renderer> ().material.color = Color.green;
		}
		deltaSelectedSpawner = selectedSpawner;
		DeltaPress[0] = KeyPress[0];
		DeltaPress[1] = KeyPress[1];
	}
}