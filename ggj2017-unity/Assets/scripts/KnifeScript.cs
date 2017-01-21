using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour {

	public float speedMin = 0.04f;
	public float speedMax = 0.05f;

	public float minLiveTime = 3;
	public float offsetX = 5;
	float startTime;

	WaveGenerator waveGenerator;

	SpriteRenderer sprite;

	float speed;
	// Use this for initialization
	void Start () {
		waveGenerator = GameObject.Find ("Main Camera").GetComponent<WaveGenerator> ();

		startTime = Time.timeSinceLevelLoad;
		sprite = GetComponent<SpriteRenderer> ();
		speed = Random.Range (speedMin, speedMax);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var pos = transform.position;
		pos.x -= speed;
		pos.y = waveGenerator.getWorldHightByX (pos.x) + offsetX;
		transform.position = pos;
	}

	void Update()
	{
		if (!sprite.isVisible && Time.timeSinceLevelLoad - startTime > minLiveTime) {
			Destroy (gameObject);
		}
	}
}
