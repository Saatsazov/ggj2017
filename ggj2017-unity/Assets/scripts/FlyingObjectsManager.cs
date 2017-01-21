using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FlyingObjectsTypes
{
    private readonly String name;
    private readonly int value;

    private static readonly Dictionary<int, FlyingObjectsTypes> instance = new Dictionary<int, FlyingObjectsTypes>();
    public static int Length = instance.Count;
    public static IEnumerable<FlyingObjectsTypes> Values = instance.Values;

    public static readonly FlyingObjectsTypes CUP = new FlyingObjectsTypes(0, "FlyingCup");
    public static readonly FlyingObjectsTypes BRA = new FlyingObjectsTypes(1, "FlyingBra");

    private FlyingObjectsTypes(int value, String name)
    {
        this.name = name;
        this.value = value;
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
    Rigidbody2D body;
    System.Random randomizer = new System.Random();
    private float nextFlyDelay = 3.0f;
    private float prevFlyDelay = 3.0f;

    void Start()
    {
        flyingObjectsTemplates = new Dictionary<FlyingObjectsTypes, GameObject>();
        foreach (FlyingObjectsTypes type in FlyingObjectsTypes.Values)
        {
            flyingObjectsTemplates.Add(type, GameObject.Find(type.ToString()));
        }
        body = GetComponent<Rigidbody2D>();
        body.angularVelocity = 120.0f;
    }

    void Update()
    {
        nextFlyDelay -= Time.deltaTime;
        if (nextFlyDelay <= 0f)
        {
            CreateRandomFlyingObject();
            prevFlyDelay -= 0.05f;
            nextFlyDelay = Mathf.Max(prevFlyDelay, 1.5f);
        }
    }

    private void CreateRandomFlyingObject()
    {
        var randomStartXPosition = randomizer.Next(400, 600) / 100.0f;
        var randomTargetXPosition = (randomizer.Next(0, 400) - 200) / 100.0f;
        var randomTargetYPosition = randomizer.Next(400, 900) / 100.0f;
        var randomObject = (FlyingObjectsTypes)(randomizer.Next() % FlyingObjectsTypes.Length);
        GameObject flyingObject = Instantiate(
            flyingObjectsTemplates[randomObject],
            new Vector3(randomStartXPosition, -3.0f),
            new Quaternion());
        var flyingObjectRigidbody = flyingObject.GetComponent<Rigidbody2D>();
        flyingObjectRigidbody.velocity = GetBallisticVelocity(
            flyingObject,
            new Vector3(randomTargetXPosition, randomTargetYPosition));
        Destroy(flyingObject, 2);
    }

    private Vector3 GetBallisticVelocity(GameObject flyingObject, Vector3 targetPosition)
    {
        var direction = targetPosition - flyingObject.transform.position;
        var heightDifference = direction.y;
        direction.y = 0;
        var distance = direction.magnitude; 
        direction.y = distance;
        distance += heightDifference;
        var vel = Mathf.Sqrt(distance * Physics.gravity.magnitude);
        return vel * direction.normalized;
    }
}
