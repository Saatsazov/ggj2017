using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatIntroScript : MonoBehaviour {

	float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.timeSinceLevelLoad;
		Invoke ("goToGame", 3.0f);
	}

	void goToGame()
	{
		SceneManager.LoadScene ("game");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad > startTime + 2) {
			var pos = transform.position;
			pos.x += 0.2f;
			transform.position = pos;
		}
	}
}
