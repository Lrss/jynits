using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : MonoBehaviour {

	private Rigidbody rb;
	public float speed = 10f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (transform.up * speed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
