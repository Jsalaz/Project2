using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

	//movement variables
	public float maxSpeed;
	public float sprintSpeed;

	//jumping variables
	bool grounded = false;
	float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight;

	// jetpack variables
	public float jetpackVelocity;
	public float maxJetpackVelocity;
	public GameObject jetpackPS;
	public Transform jetpackParticleLocation;

	//jetpack HUD
	float currentFuel;
	public float jetpackMaxFuel;
	public Slider jetpackSlider;
	public float fuelRechargeRate;
	public float fuelBurnRate;

	Rigidbody2D myRB;
	Animator myAnim;
	bool facingRight;

	// for shooting
	public Transform rocketFireLocation;
	public GameObject rocket;
	public float fireRate = 0.5f;
	public float nextFire = 0f;

	//for startOnButton
	private bool hasStarted = false;
	//private bool canFire = false;

	void Start () {
	//	myRB = GetComponent<Rigidbody2D> ();
	//	myAnim = GetComponent<Animator> ();
	//	facingRight = true;
	}

	public void startOnButton(){
		myRB = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		facingRight = true;
		hasStarted = true;

		currentFuel = jetpackMaxFuel;
		jetpackSlider.maxValue = jetpackMaxFuel;
		jetpackSlider.value = jetpackMaxFuel;
	}
		
	// Update is called once per frame
	void Update () {

		//player shooting
		if ((Input.GetAxisRaw ("Fire1") > 0) && hasStarted) {
			fireRocket ();
		}
	}


	void FixedUpdate () {

		if (hasStarted) {
			//check if we are grounded, if not, then we are falling
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);
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
			
			//Sprinting, doesnt affect jumping, so kind of pointless
			/*if (grounded && Input.GetKey (KeyCode.LeftShift)) {
			myAnim.SetFloat ("Speed", Mathf.Abs (move));
			myRB.velocity = new Vector2 (move * sprintSpeed, myRB.velocity.y);
		}
		*/
			
			// Jumping
			if (grounded && Input.GetAxis ("Jump") > 0) {
				grounded = false;
				myAnim.SetBool ("IsGrounded", grounded);
				myRB.AddForce (new Vector2 (0, jumpHeight));
			}

			//Jetpack
			if (Input.GetKey (KeyCode.LeftShift) && myRB.velocity.y < maxJetpackVelocity
			&& currentFuel > 0) {
				currentFuel -= fuelBurnRate;
				jetpackSlider.value = currentFuel;
				myRB.AddForce (new Vector2 (0, jetpackVelocity));
				Instantiate (jetpackPS, jetpackParticleLocation, false);
				}

			if (!Input.GetKey (KeyCode.LeftShift) && currentFuel <= jetpackMaxFuel) {
				currentFuel += fuelRechargeRate;
				jetpackSlider.value = currentFuel;
				}
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
