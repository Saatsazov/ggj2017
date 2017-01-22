using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScriptCamera : MonoBehaviour {

	public GameObject splash;
	public GameObject canvas;

	AudioSource audio;

	// Use this for initialization
	void Start () {
		Invoke ("hideSplash", 4.0f);
		audio = GameObject.Find ("mainMenu").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void hideSplash()
	{
		canvas.SetActive (true);
		splash.SetActive (false);
		audio.Play ();
	}
}
