using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	//movement variables
	public float maxSpeed;
	public float runSpeed;

	//jumping variables
	bool grounded = false;
	float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight;

	// jetpack variables
	public float jetpackVelocity;

	Rigidbody2D myRB;
	Animator myAnim;
	bool facingRight;

	// for shooting
	public Transform rocketFireLocation;
	public GameObject rocket;
	public float fireRate = 0.5f;
	public float nextFire = 0f;

	void Start () {
		myRB = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		facingRight = true;
	}

	// Update is called once per frame
	void Update () {

		//player shooting
		if (Input.GetAxisRaw ("Fire1") > 0) {
			fireRocket ();
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
			
		//Sprinting
		/*if (grounded && Input.GetKey (KeyCode.LeftShift)) {
			myAnim.SetFloat ("Speed", Mathf.Abs (move));
			myRB.velocity = new Vector2 (move * runSpeed, myRB.velocity.y);
		}
		*/
			
		// Jumping
		if (grounded && Input.GetAxis ("Jump") > 0) {
			grounded = false;
			myAnim.SetBool ("IsGrounded", grounded);
			myRB.AddForce (new Vector2 (0, jumpHeight));
			//wait to use Jetpack
		}

		//Jetpack
		if (Input.GetKey(KeyCode.LeftShift)) {
			myRB.AddForce(new Vector2(0,jetpackVelocity));
		}

	}
		

	void flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void fireRocket() {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			if (facingRight) {
				Instantiate (rocket, rocketFireLocation.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
			} else if (!facingRight) {
				Instantiate (rocket, rocketFireLocation.position, Quaternion.Euler (new Vector3 (0, 0, 180f)));
			}
		}
	}
		

}
