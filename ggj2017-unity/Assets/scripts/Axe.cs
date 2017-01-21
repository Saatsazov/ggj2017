using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour {
    GameObject axeTemplate;
    Rigidbody2D body;

    void Start () {
        axeTemplate = GameObject.Find("axe");
        body = GetComponent<Rigidbody2D>();
        body.angularVelocity = 120.0f;
	}
	
	void Update () {
        if (Input.GetKeyDown("a"))
        {
            // GameObject axe = Instantiate(axeTemplate, transform.position, transform.rotation);
            axeTemplate.transform.position = new Vector3(0, 0);
            var axeRigidbody = axeTemplate.GetComponent<Rigidbody2D>();
            axeRigidbody.velocity = GetBallisticVelocity(new Vector3(-3.5f, 5.0f)); // use here hero position
            // Destroy(axe, 10);
        }
    }

    private Vector3 GetBallisticVelocity(Vector3 targetPosition) {
         var dir = targetPosition - transform.position; // get target direction
            var h = dir.y;  // get height difference
            dir.y = 0;  // retain only the horizontal direction
         var dist = dir.magnitude;  // get horizontal distance
            dir.y = dist;  // set elevation to 45 degrees
         dist += h;  // correct for different heights
         var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);
         return vel* dir.normalized;  // returns Vector3 velocity
    }
}
