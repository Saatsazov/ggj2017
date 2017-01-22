using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour {

	public Hero hero;

	Animator animator;
	public GameObject head;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
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
			collider.gameObject.tag = "Finish";

			if (hero.RemoveLife (collider.gameObject.name)) {
				dieAnim ();
			} else {
				animator.SetTrigger ("bang");
			}
		}
	}

	void dieAnim()
	{
		animator.SetTrigger ("die");
		head.SetActive (true);
		head.GetComponent<Rigidbody2D> ().angularVelocity = 800.0f;
		head.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-2, 4);
		GetComponent<BoxCollider2D> ().enabled = false;
		hero.showDeath ();
		hero.wall.GetComponent<BoxCollider2D> ().enabled = false;
	}


}
