using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	public Text levelText;

	private Rigidbody rb;
	private int count;

	private Animator anim;

	GameObject[] PickUps;
	private int randomDie;

	public float vSpeed;
	public float jumpSpeed;
	private bool onGround;

	public GameObject Portal;
	public GameObject ReplayBtn;

	// private GameObject[] enemies;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
		count = 0;
		winText.text = "";
		PickUps = GameObject.FindGameObjectsWithTag("Pick Up");
		anim.SetBool("isIdle", true);

		// enemies = GameObject.FindGameObjectsWithTag("Enemy");
		// StartCoroutine(ActivateEnemy());
	}

	void FixedUpdate ()
	{
		SetCountText ();
		vSpeed = rb.velocity.y;
        anim.SetFloat ("vSpeed", vSpeed);
	}

	void Update()
	{
		if (!anim.GetBool("isDie"))
		{
			if (Input.GetKey(KeyCode.A))
			{
				var speed = 100;
				transform.Rotate(-Vector3.up * speed * Time.deltaTime);
			}
				
			if (Input.GetKey(KeyCode.D))
			{
				var speed = 100;
				transform.Rotate(Vector3.up * speed * Time.deltaTime);
			}

			if (Input.GetKey(KeyCode.W))
			{
				anim.SetBool("isRun", true);
				anim.SetBool("isIdle", false);
				if(onGround)
				{
					anim.Play("run");
				}
				//Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
				rb.velocity = transform.forward * speed;
				// rb.AddForce(transform.forward * speed);
			}

			if (Input.GetKeyUp(KeyCode.W))
			{
				anim.SetBool("isRun", false);
				anim.SetBool("isIdle", true);
				if(onGround)
				{
					anim.Play("idle");
				}
			}

			if (Input.GetKey(KeyCode.S))
			{
				anim.SetBool("isWalk", true);
				anim.SetBool("isIdle", false);
				if(onGround)
				{
					anim.Play("walk");
				}
				//Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
				rb.velocity = -transform.forward * speed / 2;
			}

			if (Input.GetKeyUp(KeyCode.S))
			{
				anim.SetBool("isWalk", false);
				anim.SetBool("isIdle", true);
				if(onGround)
				{
					anim.Play("idle");
				}
			}

			// if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y == 0)
			// {
			// 	anim.Play("jumpSplitted_B");
			// 	anim.SetBool("isJump", true);
			// 	rb.AddForce(rb.velocity.x, jumpSpeed, rb.velocity.z);

			// 	Invoke("ResetJump", 2);
				
			// }

			// rb.AddForce (movement * speed);
		}
	}

	void ResetJump ()
	{
		anim.SetBool("isJump", false);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText ()
	{
		if(PickUps != null){
			countText.text = "GEMS COLLECTED: " + count.ToString () + "/" + PickUps.Length;
			if (count >= PickUps.Length) {
				winText.text = "BRING THEM TO THE PORTAL!";
				Portal.SetActive(true);
			}
		}
		else
		{
			Debug.Log("Game Object not found...");
		}
	}

	void OnCollisionEnter(Collision other) 
	{
		if (other.gameObject.CompareTag ("Enemy")) 
		{
			winText.text = "NICE TRY, ZOMBIE!";
			ReplayBtn.SetActive(true);

			randomDie = Random.Range(0, 2);
			if (randomDie == 0)
			{
				anim.Play("die1");
			}
			else if (randomDie == 1)
			{
				anim.Play("die2");
			}

			anim.SetBool("isDie", true);
			rb.velocity = Vector3.zero;
		}
		if (other.gameObject.CompareTag ("Ground"))
		{
			onGround = true;
			// Debug.Log("on the ground!");
		}
		if (other.gameObject.CompareTag ("Portal"))
		{
			if (int.Parse(levelText.text) == 3)
			{
				SceneManager.LoadScene("winScene");
			}
			else
			{
				SceneManager.LoadScene("level" + (int.Parse(levelText.text) + 1));
			}
		}
	}

	void OnCollisionExit(Collision other)
	{
		if(other.gameObject.CompareTag("Ground"))
		{
			onGround = false;
			// Debug.Log("off the ground!");
		}
	}

	// IEnumerator ActivateEnemy()
	// {
	// 	for(int i = 0; i < enemies.Length; i ++)
	// 	{
	// 		enemies[i].SetActive(true);
	// 		Debug.Log("enemy coming...");
	// 		yield return new WaitForSeconds(1);
	// 	}
	// }
}
	