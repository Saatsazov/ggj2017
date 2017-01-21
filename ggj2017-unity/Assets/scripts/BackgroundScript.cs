using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

	public float speed;
	public float maxX = 100;

	// Update is called once per frame
	void Update () {
		var pos = transform.position;

		pos.x -= speed;

		if (pos.x < -maxX) {
			pos.x += maxX;
		}

		transform.position = pos;
	}
}
