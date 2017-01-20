using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {
    private float kAmplitude = 1f;
    private float kAmplitudePrev = 1f;
    private float previousAngle = 0f;
    private float epsilan = Mathf.PI / 36;
    private float amplitudeStep = 0.01f;

    Rigidbody2D body;
	float x;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var random = new System.Random();

        if (Mathf.Abs(previousAngle - Mathf.PI) < epsilan)
        {
            kAmplitudePrev = kAmplitude;
            kAmplitude = (float)random.NextDouble() + 0.3f;
        }

        var kAmplitudeLocal = Mathf.Abs(kAmplitude - kAmplitudePrev) < epsilan ?
            kAmplitude : kAmplitudePrev + ((kAmplitude > kAmplitudePrev) ? amplitudeStep : -amplitudeStep);
        kAmplitudePrev = kAmplitudeLocal;

        previousAngle += Mathf.PI / 36; // + 5deg

        if (Mathf.Abs(previousAngle - 2 * Mathf.PI) < epsilan)
        {
            previousAngle = 0;
        }

        x += 0.02f;
		var y = kAmplitudeLocal * Mathf.Sin(previousAngle);
//		var pos = body.position;
//		pos.y = Mathf.Sin (x);
//		body.position = pos;

		body.MovePosition(new Vector2(body.position.x, y));

//		var pos = transform.position;
//		pos.y = Mathf.Sin (x);
//		transform.position = pos;
	}
}
