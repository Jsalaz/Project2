using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	// Use this for initialization
	public float rocketSpeeed;

	Rigidbody2D myRB;

	void Awake () {
		myRB = GetComponent<Rigidbody2D> ();

		if (transform.localRotation.z > 0)
			myRB.AddForce (new Vector2 (-1, 0) * rocketSpeeed, ForceMode2D.Impulse);
		else
			myRB.AddForce (new Vector2 (1, 0) * rocketSpeeed, ForceMode2D.Impulse);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {

	}
}
