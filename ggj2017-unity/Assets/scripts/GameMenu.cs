using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {


	public GameObject pausePanel;
	bool isPaused;


	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isPaused) {
				
				resume ();
			} else {
				pause ();
			}
		}
	}

	public void pause()
	{
		isPaused = true;
		pausePanel.SetActive (true);
	}

	public void resume()
	{
		isPaused = false;
		pausePanel.SetActive (false);
	}
}
