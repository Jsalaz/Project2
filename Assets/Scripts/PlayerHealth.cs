using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float playerMaxHealth;
	public GameObject deathEffects;
	public AudioClip playerSound;


	float currentHealth;

	PlayerController controlMovement;

	//ui stuff
	public Slider healthSlider;
	public Image bloodScreen;
	private bool isDamaged;
	private Color damageColor = new Color(0f,0f,0f,0.5f);
	private float damageColorShift = 5f;

	private AudioSource playerSoundSource;

	// Use this for initialization
	void Start () {
		currentHealth = playerMaxHealth;

		controlMovement = GetComponent<PlayerController> ();

		//HUD init
		healthSlider.maxValue = playerMaxHealth;
		healthSlider.value = playerMaxHealth;
		isDamaged = false;
		//audio
		playerSoundSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		if (isDamaged) {
			bloodScreen.color = damageColor;

		} else {
			bloodScreen.color = Color.Lerp (bloodScreen.color, Color.clear, damageColorShift * Time.deltaTime);
		}
		isDamaged = false;
	}

	public void addDamage(float damage){
		if (damage <= 0)
			return;
		currentHealth -= damage;

		healthSlider.value = currentHealth;
		isDamaged = true;

		//playerSoundSource.clip = playerSound;
		//playerSoundSource.Play ();

		playerSoundSource.PlayOneShot (playerSound);

		if(currentHealth <= 0) {
			makeDead ();
			}
	}

	public void makeDead(){
		Instantiate (deathEffects, transform.position, transform.rotation);
		Destroy (gameObject);
	}

}

