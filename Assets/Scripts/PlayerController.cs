using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	//movement variables
	public float maxSpeed;

	//jumping variables
	bool grounded = false;
	float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight;


	Rigidbody2D myRB;
	Animator myAnim;
	bool facingRight;

	void Start () {
		myRB = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		facingRight = true;
	}

	// Update is called once per frame
	void Update () {
		if (grounded && Input.GetAxis ("Jump") > 0) {
			grounded = false;
			myAnim.SetBool ("IsGrounded", grounded);
			myRB.AddForce (new Vector2 (0, jumpHeight));
		}
	}


	void FixedUpdate () {

		//check if we are grounded, if not, then we are falling
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		myAnim.SetBool ("IsGrounded", grounded);

		myAnim.SetFloat ("VerticalSpeed", myRB.velocity.y);


		float move = Input.GetAxis ("Horizontal");
		myAnim.SetFloat ("Speed", Mathf.Abs (move));
		myRB.velocity = new Vector2 (move * maxSpeed, myRB.velocity.y);

		if (move > 0 && !facingRight) {
			flip ();
		} else if (move < 0 && facingRight) {
			flip ();
		}
	}


	void flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


}
