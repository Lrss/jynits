using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
	idle,
	attack,
	walk
}

public class Attack : MonoBehaviour {
	public State currentState = State.idle;
	GameObject currentTarget;
	public GameObject enemySpawner;
	public GameObject mySpawner;
	List<GameObject> targets = new List<GameObject>();
	Animator anim;   
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void FaceTarget(GameObject target){
		transform.LookAt(target.transform);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag != tag && other.tag !=  "Untagged") {
			Debug.Log (other);
			targets.Add (other.gameObject);
		}
	}

	void Update () {
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
			if (Vector3.Distance (currentTarget.transform.position, transform.position) < 1) {
				currentState = State.attack;
			} else {
				FaceTarget (currentTarget);
			}
			break;
		case State.attack:
			anim.SetBool ("isMoving", false);
			FaceTarget (currentTarget);
			if (Vector3.Distance (currentTarget.transform.position, transform.position) < 1) {
				if (currentTarget == enemySpawner) {
					Debug.Log("Player " + tag + " Vandt en LANE!");
<<<<<<< HEAD
					Destroy(currentTarget);                    
=======
					Destroy(currentTarget,0.5f);
>>>>>>> 4f1a01e558977ef32eb5fde31ad3657f810a2c5f
				}
				//else
					//ApplyDamage
				
			} else {
				currentState = State.walk;
				FaceTarget (currentTarget);
			}
			break;
		default:
			Debug.LogError ("Unit state fucked up!");
			break;
		}
	}
}
