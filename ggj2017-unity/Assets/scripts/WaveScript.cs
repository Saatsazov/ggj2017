using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour {

	GameObject hand;
	// Use this for initialization
	void Start () {
		hand = GameObject.Find ("hand");
		print (hand);

//		Transform t = new Transform ();

		for (int i = 0; i < 100; i++) {
			var newHand = Instantiate (hand);
			newHand.transform.position = new Vector3 (10 - i, -5 + Mathf.Sin(i));
			float scale = Random.Range (0.1f, 0.2f);
//			float scale = 0.2f;
			newHand.transform.localScale = new Vector3 (scale, scale, scale);

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
