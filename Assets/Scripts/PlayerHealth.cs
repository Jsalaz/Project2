using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float playerMaxHealth;
	public GameObject deathEffects;

	float currentHealth;

	PlayerController controlMovement;

	// Use this for initialization
	void Start () {
		currentHealth = playerMaxHealth;

		controlMovement = GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addDamage(float damage){
		if (damage <= 0)
			return;
		currentHealth -= damage;

		if(currentHealth <= 0) {
			makeDead ();
			}
	}

	public void makeDead(){
		Instantiate (deathEffects, transform.position, transform.rotation);
		Destroy (gameObject);
	}

}

