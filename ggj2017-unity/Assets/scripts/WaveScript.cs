using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour {
    public WaveGenerator waveGenarator;
    GameObject handTemplate;
	private GameObject[] hands;
	public int handsCount = 20;
	public float xSpeed = 0.4f;
	public float waveCameraOffset = 2;

	GameObject emptyWave;

	public GameObject wall;

    void Start () {
        handTemplate = GameObject.Find("hand");
		GenerateHands ();
		addEmptyWave ();
	}
	
	// Update is called once per frame
	void Update () {

		var cameraPos = transform.position;
		cameraPos.y = waveGenarator.getWorldHightByX(wall.transform.position.x);
		transform.position = cameraPos;

		for (int i = 0; i < handsCount; i++) {
			var hand = hands [i];
			var pos = hand.transform.position;
			var newPosY = waveGenarator.getWorldHightByX (pos.x);

			pos.y = newPosY - cameraPos.y - waveCameraOffset;

			pos.x -= xSpeed;
			if (pos.x < -10) {
				pos.x += 20;
			}
			hand.transform.position = pos;

			if (emptyWave != null) {
				var w = 2.0f;
				if ((pos.x < emptyWave.transform.position.x + w) &&
				    (pos.x > emptyWave.transform.position.x - w)) {
					hand.SetActive (false);
				} else {
					hand.SetActive (true);
				}
					
			}
		}

    }

	public void addEmptyWave()
	{
		emptyWave = GameObject.Find("EmptyWave");
	}

	void GenerateHands()
	{
		hands = new GameObject[handsCount];

		for (int i = 0; i < handsCount; i++) {
			var newHand = Instantiate (handTemplate); 
			newHand.transform.position = new Vector3 (-10 + i, -5 + Mathf.Sin(i), 1); 
			float scale = Random.Range (0.1f, 0.2f); 
			newHand.transform.localScale = new Vector3 (scale, scale, scale);
			hands [i] = newHand;
		} 
	}
}
