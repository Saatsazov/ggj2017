using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour {

	public Hero hero;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "wall") {
			hero.isBoatGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.name == "wall") {
			hero.isBoatGrounded = false;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "dieElements") {
			// hero.gameOver ();
		}
	}


}
