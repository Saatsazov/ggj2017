using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour {
    public WallScript wallScr;
    GameObject handTemplate;
    private Queue<GameObject> hands = new Queue<GameObject>();
    void Start () {
        handTemplate = GameObject.Find ("hand");
        GenerateHands();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateHands();
    }

    private void UpdateHands()
    {
        hands.Enqueue(CreateHand(wallScr.GetLevelByIndex(hands.Count - 1), hands.Count - 1));
        var handToDelete = hands.Dequeue();
        Destroy(handToDelete);
        var enumerator = hands.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var hand = enumerator.Current;
            hand.transform.position = new Vector3(hand.transform.position.x - 1, hand.transform.position.y);
        }
    }

    private void GenerateHands()
    {
        var index = 0;
        var enumerator = wallScr.levelsList.GetEnumerator();
        while (enumerator.MoveNext())
        {
            hands.Enqueue(CreateHand(enumerator.Current, index++));
        }
    }

    private GameObject CreateHand(float height, float index)
    {
        var hand = Instantiate(handTemplate);
        hand.transform.position = new Vector3(-10 + index, -5 + height);
        var scale = 0.1f;
        hand.transform.localScale = new Vector3(scale, scale, scale);
        return hand;
    }
}
