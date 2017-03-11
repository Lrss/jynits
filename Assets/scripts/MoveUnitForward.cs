using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitForward : MonoBehaviour {

	Rigidbody rb;
	public bool turn;

	void Start () {
		rb = GetComponent<Rigidbody>();	
	}
	
	void Update () {
		rb.AddForce ((turn ? Vector3.right : Vector3.left)*500 * Time.deltaTime);
	}
}
