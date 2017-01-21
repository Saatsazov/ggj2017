using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour {

	public float repeatTime = 0.5f;
    public int numberOfLevels = 54;

    public int heroLevelIndex = 25;
   
	public Queue<float> levelsList = new Queue<float>();

    private float kAmplitude = 1; // amplitude!
    private float kFrequency = 1.2f; // velocity!
    private float kAmplitudePrev = 1;
    private float kFrequencyPrev = 1.2f;
    private float previousAngle = 0f;
    private float epsilan = 0.001f;
    private float amplitudeStep = 0.1f;
    private float currentLevel = 0;
    private float angleStep = Mathf.PI/18;

	// Use this for initialization
	void Start () {
        GenerateLevelsList();
		InvokeRepeating ("AddNewLevel", 0,  repeatTime );
    }
	
	// Update is called once per frame
	void Update () {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
		//var y = getWorldHightByX (transform.position.x);
		//print("camera y: " + y);
		//transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    private float NextAmplitude()
    { 
        var random = new System.Random();

        if (Mathf.Abs(currentLevel) < epsilan)
        {
            kAmplitudePrev = kAmplitude;
            kAmplitude = (float)random.NextDouble() + 0.3f;
            // kFrequencyPrev = kFrequency;
            // kFrequency = (float)random.NextDouble() + 0.7f;
            angleStep = Mathf.PI / random.Next(10, 20);
        }

        var kAmplitudeLocal = Mathf.Abs(kAmplitude - kAmplitudePrev) < epsilan ?
            kAmplitude : kAmplitudePrev + ((kAmplitude > kAmplitudePrev) ? amplitudeStep : -amplitudeStep);
        kAmplitudePrev = kAmplitudeLocal;

        var kFrequencyLocal = Mathf.Abs(kFrequency - kFrequencyPrev) < epsilan ?
            kFrequency : kFrequencyPrev + ((kFrequency > kFrequencyPrev) ? amplitudeStep : -amplitudeStep);
        kFrequencyPrev = kFrequencyLocal;

        previousAngle += angleStep; //Mathf.PI / 18; // real frequency!

        if (Mathf.Abs(previousAngle - 2 * Mathf.PI / kFrequency) < epsilan)
        {
            previousAngle = 0;
        }

        currentLevel = kAmplitudeLocal * Mathf.Sin(previousAngle * kFrequencyLocal);

        return currentLevel;
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

    public float GetLevelByIndex(int index)
    {
        var enumerator = levelsList.GetEnumerator();
        var i = 0;
        while (i++ <= index) enumerator.MoveNext();
        return enumerator.Current;
    }

	public float getWorldHightByX(float x)
	{
		float sceneWidth = 18.0f;
		int index = (int)((x + sceneWidth/2)  * ((float)numberOfLevels/sceneWidth));
		index = Mathf.Max (0, index);
		index = Mathf.Min (numberOfLevels-1, index);
		return GetLevelByIndex (index);
	}
}
