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
	GameObject mainCamera;

	static bool isTemplateCreated;
	bool isTemplate;

	Sprite []knifePatterns;

	float speed;
	// Use this for initialization
	void Start () {
		isTemplate = !isTemplateCreated;
		isTemplateCreated = true;

		mainCamera = GameObject.Find ("Main Camera");
		waveGenerator = mainCamera.GetComponent<WaveGenerator> ();

		startTime = Time.timeSinceLevelLoad;
		sprite = GetComponent<SpriteRenderer> ();
		speed = Random.Range (speedMin, speedMax);
        transform.position = new Vector3(9, -2.0f);

		knifePatterns = Resources.LoadAll<Sprite> ("badHands");

		var index = Random.Range (0, knifePatterns.Length);
		GetComponent<SpriteRenderer>().sprite = knifePatterns [index];
		if (index == 0) {
			tag = "duck";
			name += "Duck";
		} else {
			tag = "dieElements";
		}
	}

	void Update()
	{
		if(isTemplate)
		{
			transform.position = new Vector3 (100, 100, 2.4f);
			return;
		}

        var pos = transform.position;
        pos.x -= speed;
        pos.y = mainCamera.transform.position.y + offsetX;
		pos.z = 2.4f;
        transform.position = pos;
        if (!sprite.isVisible && Time.timeSinceLevelLoad - startTime > minLiveTime) {
			//Destroy (gameObject);
		}
	}
}
