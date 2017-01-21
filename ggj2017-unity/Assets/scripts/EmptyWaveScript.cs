using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyWaveScript : MonoBehaviour {


	public float speed = 0.01f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var pos = transform.position;
		pos.x -= speed;
		transform.position = pos;
	}
}
