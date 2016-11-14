using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public float enemyMaxHealth;

	float currentHealth;

	//enemyHealth UI
	public GameObject enemyDeathFX;
	public Slider enemySlider;
	//enemyAudio
	public AudioSource enemyAS;

	// Use this for initialization
	void Start () {
		currentHealth = enemyMaxHealth;
		enemySlider.maxValue = enemyMaxHealth;
		enemySlider.value = currentHealth;
		enemySlider.gameObject.SetActive (false);
		enemyAS = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addDamage(float damage){
		enemySlider.gameObject.SetActive (true);

		currentHealth -= damage;
		//enemyAS.Play ();

		enemySlider.value = currentHealth;
		if (currentHealth <= 0) {
			makeDead ();
		}
	}

	void makeDead(){
		Destroy (gameObject);
		Instantiate (enemyDeathFX, transform.position, transform.rotation);
	}
}
