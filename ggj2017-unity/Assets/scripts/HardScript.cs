using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardScript : MonoBehaviour {

	GameObject mainCamera;
	public float offset = 2;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		var pos = transform.position;
		pos.y = mainCamera.transform.position.y + offset;
		transform.position = pos;
	}
}
