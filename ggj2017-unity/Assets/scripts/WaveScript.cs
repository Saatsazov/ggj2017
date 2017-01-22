using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour {
    public WaveGenerator waveGenarator;
    GameObject handTemplate;
	private GameObject[] hands;
	public int handsCount = 40;
	public float xSpeed = 0.4f;
	public float waveCameraOffset = 2;

	GameObject emptyWave;

	Sprite[] handSprites;

	public GameObject wall;

    void Start () {
        handTemplate = GameObject.Find("hand");
		handSprites = Resources.LoadAll<Sprite> ("hands");

		GenerateHands ();
		addEmptyWave ();

	}
	
	// Update is called once per frame
	void Update () {

		var cameraPos = transform.position;
		cameraPos.y = - waveGenarator.getWorldHightByX(wall.transform.position.x);
		transform.position = cameraPos;

		for (int i = 0; i < handsCount; i++) {
			var hand = hands [i];
			var pos = hand.transform.position;
			var newPosY = waveGenarator.getWorldHightByX (pos.x);

			pos.y = newPosY + cameraPos.y - waveCameraOffset + hand.GetComponent<HandScript> ().offsetY;


			pos.x -= xSpeed;
			if (pos.x < -10) {
				pos.x += 20;
				var index = Random.Range (0, handSprites.Length - 1);
				hand.GetComponent<SpriteRenderer> ().sprite = handSprites[index];
				float scale = Random.Range (0.5f, 0.7f); 
				hand.transform.localScale = new Vector3 (scale, scale, scale);

				if (index > handSprites.Length - 4) {
					pos.z = 1;
				} else {
					pos.z = 2;
				}

				hand.GetComponent<HandScript> ().offsetY = Random.Range (-1, 1);
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
			var index = Random.Range (0, handSprites.Length - 1);
			var newHand = Instantiate (handTemplate); 
			var pos = new Vector3 (-10 + i, -5 + Mathf.Sin (i), 1);
			
			if (index > handSprites.Length - 4) {
				pos.z = 1;
			} else {
				pos.z = 2;
			}

			newHand.transform.position = pos; 
			float scale = Random.Range (0.5f, 0.9f); 
			newHand.transform.localScale = new Vector3 (scale, scale, scale);
			newHand.GetComponent<SpriteRenderer> ().sprite = handSprites [index];
			newHand.GetComponent<HandScript> ().offsetY = Random.Range (-1, 1);
			hands [i] = newHand;
		} 
	}
}
