﻿using UnityEngine;
using System.Collections;

public class RocketHit : MonoBehaviour {

	public float weaponDamage;

	ProjectileController myPC;

	public GameObject explosionEffect;

	void Awake () {
		myPC = GetComponentInParent<ProjectileController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == LayerMask.NameToLayer ("Shootable")) {
			myPC.removeForce ();
			Instantiate (explosionEffect, transform.position, transform.rotation);
			Destroy (gameObject);
			if (other.tag == "Enemy") {
				EnemyHealth hurtEnemy = other.gameObject.GetComponent<EnemyHealth> ();
				hurtEnemy.addDamage (weaponDamage);
			}
		}
		if (other.tag == "Shootable") {
			myPC.removeForce ();
			Instantiate (explosionEffect, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.layer == LayerMask.NameToLayer ("Shootable")) {
			myPC.removeForce ();
			Instantiate (explosionEffect, transform.position, transform.rotation);
			Destroy (gameObject);
			if (other.tag == "Enemy") {
				EnemyHealth hurtEnemy = other.gameObject.GetComponent<EnemyHealth> ();
				hurtEnemy.addDamage (weaponDamage);
			}
	}
		if (other.tag == "Shootable") {
			myPC.removeForce ();
			Instantiate (explosionEffect, transform.position, transform.rotation);
			Destroy (gameObject);
		}
}
}