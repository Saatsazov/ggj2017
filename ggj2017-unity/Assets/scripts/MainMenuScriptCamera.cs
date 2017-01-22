using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScriptCamera : MonoBehaviour {

	public GameObject splash;
	public GameObject canvas;

	AudioSource audio;

	public static bool isAlreadyShown;

	// Use this for initialization
	void Start () {
		if (isAlreadyShown) {
			hideSplash ();
		}
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
		if (!isAlreadyShown) {
			audio.Play ();
		}
	}

	public void onClickStart()
	{
		if (isAlreadyShown) {
			SceneManager.LoadScene ("game");
		} else {
			SceneManager.LoadScene ("intro");
		}

		isAlreadyShown = true;
	}
}
