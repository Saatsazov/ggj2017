using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

	public BoxCollider2D upperCollider;
	public BoxCollider2D bottomCollider;
	public Rigidbody2D rigitBody;
	public Rigidbody2D boat;

	bool isGrounded;
	public bool isBoatGrounded;
	bool doubleJumpReady;

	public float maxDelay;
	public float maxDelyForBent;

	bool bentingDown;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			Invoke ("bentDown", maxDelyForBent);
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("bentDown");
			if (bentingDown) {
				bentingDown = false;
				bentUp ();
			}
			else {
				if (doubleJumpReady) {
					CancelInvoke ("jump");
					doubleJump ();
				} else {
					Invoke ("jump", maxDelay);
					doubleJumpReady = true;
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "boat") {
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.name == "boat") {
			isGrounded = false;
		}
	}

	void bentDown()
	{
		bentingDown = true;
		upperCollider.enabled = false;
		print ("bentDown");
	}

	void bentUp()
	{
		print ("bentUp");
		upperCollider.enabled = true;

	}

	void jump()
	{
		doubleJumpReady = false;
		if (!isGrounded || !isBoatGrounded) {
			return;
		}
		Vector2 force = new Vector2 (0, 200);
		rigitBody.AddForce (force);
	}

	void doubleJump()
	{
		doubleJumpReady = false;
		if (!isGrounded || !isBoatGrounded) {
			return;
		}
		Vector2 force = new Vector2 (0, 200);
		rigitBody.AddForce (force);
		boat.AddForce (force);
		
	}
}
