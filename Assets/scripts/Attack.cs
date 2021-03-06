﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
	idle,
	attack,
	walk
}

public class Attack : MonoBehaviour {

	public int health = 100;

	public State currentState = State.idle;
	GameObject currentTarget;
	public GameObject enemySpawner;
	public GameObject mySpawner;
	List<GameObject> targets = new List<GameObject>();
	Animator anim;
	void Start () {
		anim = GetComponent<Animator> ();
	}

	public void ApplyDamage(int amount){
		health -= amount;
		Debug.Log (name + " have " + health +"");
		if (health < 0) {
			anim.SetBool ("isDead", true);
            Fabric.EventManager.Instance.PostEvent("UnitKilled", gameObject);
            Destroy (gameObject);
		}
	}

	void FaceTarget(GameObject target){
		transform.LookAt(target.transform);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "3") {
			GameObject unitt = other.transform.parent.gameObject;
			if (unitt.tag != tag && unitt.tag != "Untagged") {
				Debug.Log (tag + " is attacking " + unitt.tag + " now!");
				targets.Add (unitt.gameObject);
			}
		}
		else if(other.tag == "spear"){
			Destroy (gameObject);
		}
	}
	float attacktimer;
	void Update () {
		if (ScoreManager.player01Score > 1 || ScoreManager.player02Score > 1) {
			Destroy (this.gameObject);
			return;
		}
		if (enemySpawner == null || mySpawner == null) {
			Destroy (this.gameObject);
			return;
		}
		currentTarget = enemySpawner;
		if (targets.Count > 0) {
			for (int i = targets.Count - 1; i >= 0; i--)
			{
				if (targets [i] == null)
					targets.RemoveAt (i);
				else {
					if (Vector3.Distance (currentTarget.transform.position, transform.position) > 
						Vector3.Distance (targets [i].transform.position, transform.position))
						currentTarget = targets [i];
				}
			}
		}

		switch (currentState) {
		case State.idle:
			if (enemySpawner != null) {
				currentState = State.walk;
			}
			break;//Fuck
		case State.walk:
			anim.SetBool ("isMoving", true);
			if (Vector3.Distance (currentTarget.transform.position, transform.position) < 2.8f) {
				currentState = State.attack;
				attacktimer = Time.time * 1000;
			} else {
				FaceTarget (currentTarget);
			}
			break;
		case State.attack:
			anim.SetBool ("isMoving", false);
			FaceTarget (currentTarget);
			if (Vector3.Distance (currentTarget.transform.position, transform.position) < 3){
				if (currentTarget == enemySpawner) {
					Debug.Log ("Player " + tag + " Vandt en LANE!");
                    Fabric.EventManager.Instance.PostEvent("LaneWon", gameObject);
					if (mySpawner.transform.parent.GetComponent<PlayerController> ().playernr == 1)
						ScoreManager.player01Score += 1;
					else
						ScoreManager.player02Score += 1;
					Destroy (enemySpawner);
					Destroy (mySpawner);
					Destroy (this);
				}
				else {
					//Debug.Log (Time.time * 1000 - attacktimer);
					if (Time.time * 1000 - attacktimer >= 900){
						currentTarget.gameObject.GetComponent<Attack>().ApplyDamage(40);
						attacktimer = Time.time *1000;
					}
				}
				
			} else {
				currentState = State.walk;
				attacktimer = 0;
				FaceTarget (currentTarget);
			}
			break;
		default:
			Debug.LogError ("Unit state fucked up!");
			break;
		}
	}
}