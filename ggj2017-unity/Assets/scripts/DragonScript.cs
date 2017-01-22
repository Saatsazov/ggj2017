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
	float offset;


	static bool isTemplateCreated;
	bool isTemplate;

	// Use this for initialization
	void Start () {

		isTemplate = !isTemplateCreated;
		isTemplateCreated = true;

		startTime = Time.timeSinceLevelLoad;
		mainCamera = GameObject.Find ("Main Camera");

		sprite = GetComponent<SpriteRenderer> ();
        transform.position = new Vector3(9, 2.0f);
		offset = Random.Range(1, 4);
	}
	
	// Update is called once per frame
	void Update () {
		if(isTemplate)
		{
			transform.position = new Vector3 (100, 100, 2.4f);
			return;
		}

		var pos = transform.position;
		pos.x -= speed;
		pos.y = mainCamera.transform.position.y + offset;
		transform.position = pos;

		if (!sprite.isVisible && Time.timeSinceLevelLoad - startTime > minLiveTime) {
			//Destroy (gameObject);
		}
	}
}
