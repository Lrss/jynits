using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public string[] inputkeys = new string[3] ;
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
	bool[] KeyPress = new bool[3];
	bool[] DeltaPress = new bool[3];
	void Update () {
		KeyPress[0] = Input.GetKey(inputkeys[0]);
		KeyPress[1] = Input.GetKey(inputkeys[1]);
		KeyPress[2] = Input.GetKey(inputkeys[2]);

		if(KeyPress[2] && !DeltaPress[2]){
			var gb = Instantiate(unit, Spawners[selectedSpawner].transform.position + Vector3.up*2, Quaternion.identity);
			if (transform.position.x < 0)
				gb.GetComponent<MoveUnitForward> ().turn = true;
		}

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
		DeltaPress[2] = KeyPress[2];
	}
}