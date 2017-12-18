using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public GameObject targ;
	public Transform target;

	private GameObject[] enemies;
	// private bool collide;

	// Use this for initialization
	void Start () {
		// collide = false;
		GetComponentInChildren<Animator> ().Play("walk");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update(){
		if (targ.GetComponent<Animator>().GetBool("isDie"))
		{
			for(int i = 0; i < enemies.Length; i ++)
			{
				enemies[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
				if(!enemies[i].GetComponent<Animator>().GetBool("isAttack"))
				{
					enemies[i].GetComponentInChildren<Animator> ().Play("idle");
				}
				// else
				// {
				// 	yield WaitForSeconds(1);
				// 	enemies[i].GetComponentInChildren<Animator> ().Play("idle");
				// }
			}
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, targ.transform.position, 0.1f);
			transform.LookAt(target.transform.position);
		}
	}

	void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			GetComponentInChildren<Animator> ().Play("attack");
			GetComponentInChildren<Animator> ().SetBool("isAttack", true);
			// Invoke("ResetWalk", 1);
			// collide = true;
		}
	}

	// private void ResetWalk ()
	// {
	// 	GetComponentInChildren<Animator> ().Play("walk");
	// }
}
