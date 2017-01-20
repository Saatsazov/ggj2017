using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {

	Rigidbody2D body;
	float x;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		x += 0.02f;
		var y = Mathf.Sin (x);
//		var pos = body.position;
//		pos.y = Mathf.Sin (x);
//		body.position = pos;

		body.MovePosition(new Vector2(body.position.x, y));

//		var pos = transform.position;
//		pos.y = Mathf.Sin (x);
//		transform.position = pos;
	}
}
