using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

	public float speed;
	public float maxX = 100;

	public GameObject camera;
	WaveGenerator waveGenerator;

	void Start() {
		waveGenerator = camera.GetComponent<WaveGenerator> ();
	}

	// Update is called once per frame
	void Update () {
		var pos = transform.position;

		pos.x -= speed;

		if (pos.x < -maxX) {
			pos.x += maxX;
		}

//		pos.y = -waveGenerator.getWorldHightByX (camera.transform.position.x);
		pos.y = camera.transform.position.y;

		transform.position = pos;
	}
}
