using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour {
    private const int numberOfLevels = 27;
    private const int heroLevelIndex = 13;
    private Queue<float> levelsList = new Queue<float>();

    private float kAmplitude = 1;
    private float kAmplitudePrev = 1;
    private float previousAngle = 0f;
    private float epsilan = Mathf.PI / 36;
    private float amplitudeStep = 0.01f;

    Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
        GenerateLevelsList();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        AddNewLevel();
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        body.MovePosition(new Vector2(body.position.x, GetLevelByIndex(heroLevelIndex)));
    }

    private float NextAmplitude()
    { 
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

        return kAmplitudeLocal * Mathf.Sin(previousAngle);
    }

    private void GenerateLevelsList()
    {
        for(var i = 0; i < numberOfLevels; ++i)
        {
            levelsList.Enqueue(NextAmplitude());
        }
    }

    private void AddNewLevel()
    {
        levelsList.Enqueue(NextAmplitude());
        levelsList.Dequeue();
    }

    private float GetLevelByIndex(int i)
    {
        var enumerator = levelsList.GetEnumerator();
        var i = 0;
        while (i++ < i) enumerator.MoveNext();
        return enumerator.Current;
    }
}
