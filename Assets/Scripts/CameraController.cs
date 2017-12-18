using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public Transform target;

	// private Vector3 offset;

	// Use this for initialization
	void Start () {
		// offset = transform.position;
		
	}
	
	// Update is called once per frame, LateUpdate guarantees that it updates after everything is rendered
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			var speed = 10;
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
		// if (Input.GetKeyUp(KeyCode.LeftArrow))
		// {
		// 	transform.Translate(0, 1.2, -1.5);
		// }
		if (Input.GetKey(KeyCode.RightArrow))
		{
			var speed = 10;
			transform.Translate(Vector3.left * speed * Time.deltaTime);
		}

		transform.LookAt(target);
	}
}
