using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : MonoBehaviour {

	public float speed = 0.01f;
	WaveGenerator waveGenerator;

	public float minLiveTime = 3;

	SpriteRenderer sprite;

	float startTime;
	GameObject mainCamera;

	// Use this for initialization
	void Start () {

		startTime = Time.timeSinceLevelLoad;
		mainCamera = GameObject.Find ("Main Camera");

		sprite = GetComponent<SpriteRenderer> ();
        transform.position = new Vector3(9, 2.0f);
    }
	
	// Update is called once per frame
	void Update () {
		var pos = transform.position;
		pos.x -= speed;
		pos.y = mainCamera.transform.position.y;
		transform.position = pos;

		if (!sprite.isVisible && Time.timeSinceLevelLoad - startTime > minLiveTime) {
			//Destroy (gameObject);
		}
	}
}
