using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : MonoBehaviour {

	public float speed = 0.01f;
	public WaveGenerator waveGenerator;

	GameObject camera;

	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		var pos = transform.position;
		pos.x -= speed;
		pos.y -= waveGenerator.getWorldHightByX (camera.transform.position.x) * 0.03f;
		transform.position = pos;
	}
}
