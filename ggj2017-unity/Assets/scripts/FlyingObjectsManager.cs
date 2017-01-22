using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FlyingObjectsTypes
{
    private readonly String name;
    private readonly int value;
    public readonly int lifeDelta;
    public readonly float frequency;
    public readonly bool hasOwnVelocity;
    public readonly string objectSoundName;

    private static readonly Dictionary<int, FlyingObjectsTypes> instance = new Dictionary<int, FlyingObjectsTypes>();
    public static int Length = instance.Count;
    public static IEnumerable<FlyingObjectsTypes> Values = instance.Values;

    public static readonly FlyingObjectsTypes CUP = new FlyingObjectsTypes(0, "FlyingCup", -1, 2, false, "cupfly");
	public static readonly FlyingObjectsTypes BRA = new FlyingObjectsTypes(1, "FlyingBra", -1, 3, false, "chainbrafly");
	public static readonly FlyingObjectsTypes DRAGON = new FlyingObjectsTypes(2, "Dragon", -1, 5, true, "droginfly");
    public static readonly FlyingObjectsTypes KNIFE = new FlyingObjectsTypes(3, "Knife", -1, 5, true, "cupfly");

    private FlyingObjectsTypes(int value, String name,
        int lifeDelta, float frequency, bool hasOwnVelocity,
        string objectSoundName)
    {
        this.name = name;
        this.value = value;
        this.lifeDelta = lifeDelta;
        this.frequency = frequency;
        this.hasOwnVelocity = hasOwnVelocity;
        this.objectSoundName = objectSoundName;
        instance[value] = this;
        Length = instance.Count;
        Values = instance.Values;
    }

    public override String ToString()
    {
        return name;
    }

    public static explicit operator FlyingObjectsTypes(int value)
    {
        FlyingObjectsTypes result;
        if (instance.TryGetValue(value, out result))
            return result;
        else
            throw new InvalidCastException();
    }

}

public class FlyingObjectsManager : MonoBehaviour {

    Dictionary<FlyingObjectsTypes, GameObject> flyingObjectsTemplates;
    Dictionary<FlyingObjectsTypes, AudioSource> flyingObjectsAudio;
    System.Random randomizer = new System.Random();
    private float nextFlyDelay = 5.0f;
    private float delaySpeed = 2.0f;
    private float delaySpeedStep = 0.01f;

    private FlyingObjectsTypes nextRandomObject;
    private AudioSource objectSound;

    void Start()
    {
        nextRandomObject = (FlyingObjectsTypes)(randomizer.Next() % FlyingObjectsTypes.Length);

        flyingObjectsTemplates = new Dictionary<FlyingObjectsTypes, GameObject>();
        flyingObjectsAudio = new Dictionary<FlyingObjectsTypes, AudioSource>();

        foreach (FlyingObjectsTypes type in FlyingObjectsTypes.Values)
        {
			var o = GameObject.Find(type.ToString());
            flyingObjectsTemplates.Add(type, o);
            flyingObjectsAudio.Add(type, GameObject.Find(type.objectSoundName).GetComponent<AudioSource>());
        }

    }

    void Update()
    {
        nextFlyDelay -= Time.deltaTime;
        if (nextFlyDelay <= 0f)
        {
			CreateRandomFlyingObject();
            nextFlyDelay = delaySpeed * nextRandomObject.frequency;
            delaySpeed  = delaySpeed > 0.8 ? delaySpeed -= delaySpeedStep : delaySpeed;
        }
    }

    private void CreateRandomFlyingObject()
    {
        var randomStartXPosition = randomizer.Next(400, 600) / 100.0f;
        var randomTargetXPosition = (randomizer.Next(0, 400) - 200) / 100.0f;
        var randomTargetYPosition = randomizer.Next(700, 1000) / 100.0f;

        GameObject flyingObject = Instantiate(flyingObjectsTemplates[nextRandomObject]);

        if(!nextRandomObject.hasOwnVelocity)
        {
            flyingObject.transform.position = new Vector3(randomStartXPosition, -3.0f);
            var flyingObjectRigidbody = flyingObject.GetComponent<Rigidbody2D>();

            flyingObjectRigidbody.velocity = GetBallisticVelocity(
                new Vector3(flyingObject.transform.position.x, flyingObject.transform.position.y),
                new Vector3(randomTargetXPosition, randomTargetYPosition));

            
        }
		if (nextRandomObject == FlyingObjectsTypes.CUP || nextRandomObject == FlyingObjectsTypes.BRA) {
			var body = flyingObject.GetComponent<Rigidbody2D> ();
			body.angularVelocity = 120.0f;
		}

        flyingObjectsAudio[nextRandomObject].Play();

        Destroy(flyingObject, 6);

        nextRandomObject = (FlyingObjectsTypes)(randomizer.Next() % FlyingObjectsTypes.Length);
    }

    private Vector3 GetBallisticVelocity(Vector3 flyingObjectPosition, Vector3 targetPosition)
    {
        var direction = targetPosition - flyingObjectPosition;
        var heightDifference = direction.y;
        direction.y = 0;
        var distance = direction.magnitude; 
        direction.y = distance;
        distance += heightDifference;
        var vel = Mathf.Sqrt(distance * Physics.gravity.magnitude);
        return vel * direction.normalized;
    }
}
