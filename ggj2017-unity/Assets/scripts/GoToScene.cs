using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {

	public void goToSceneById(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}

	public void goToSceneByName(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}
