using UnityEngine;
using System.Collections;

public class HogMovementController : MonoBehaviour {

	public float hogSpeed;
	private Animator hogAnimator;

	public GameObject hogGraphic;
	private bool canFlip = true;
	private bool facingRight = false;
	private float flipTime = 5f;
	private float nextFlip = 0f;

	public float chargeTime;
	private float startCharge;
	private bool isCharging;
	private Rigidbody2D hogRB;

	// Use this for initialization
	void Start () {
		hogAnimator = GetComponentInChildren<Animator> ();
		hogRB = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextFlip){
			if (Random.Range (0, 10) >= 5) {
				flipFacing ();
			}
			nextFlip = Time.time + flipTime;				
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && gameObject != null) {
			if (facingRight && other.transform.position.x < transform.position.x) {
				flipFacing ();
			} else if (!facingRight && other.transform.position.x > transform.position.x) {
				flipFacing ();
			}
			canFlip = false;
			isCharging = true;
			startCharge = Time.time + chargeTime;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			if(startCharge >= Time.time && gameObject != null){
				if (!facingRight) {
					hogRB.AddForce (new Vector2 (-1, 0) * hogSpeed);
					hogAnimator.SetBool ("isCharging", isCharging);
				} else {
					hogRB.AddForce(new Vector2(1, 0)*hogSpeed);
					hogAnimator.SetBool ("isCharging", isCharging);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player" && gameObject != null) {
			canFlip = true;
			isCharging = false;
			hogRB.velocity = new Vector2 (0f, 0f);
			hogAnimator.SetBool ("isCharging" , isCharging);
		}
	}

	void flipFacing(){
		if (!canFlip) {
			return;
		}

		float facingX = hogGraphic.transform.localScale.x;
		facingX *= -1f;
		hogGraphic.transform.localScale = new Vector3 (facingX, hogGraphic.transform.localScale.y, hogGraphic.transform.localScale.z);
		facingRight = !facingRight;
	}
}
