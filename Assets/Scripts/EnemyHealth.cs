using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public float enemyMaxHealth;

	float currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = enemyMaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addDamage(float damage){
		currentHealth -= damage;
		if (currentHealth <= 0)
			makeDead ();
	}

	void makeDead(){
		Destroy (gameObject);
	}
}
