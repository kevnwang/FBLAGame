using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Class to monitor the player's health
public class PlayerHealth: MonoBehaviour {
	public int startingHealth = 100;

	public static int lives = Constants.STARTINGLIVES;

	// UI References
	Image[] hearts;
	Image damageImage;
	Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	// Component References
	Animator animPlayer;     // Reference to the player Animator Controller
	AudioSource[] playerAudio;     // Array of sounds that the player can make during the game

	// Script References
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;

	// Player attributes
	bool isDead;
	bool damaged;
	int playerCurrentHealth;

	void Awake() {
		hearts = GameObject.Find("HealthUI").GetComponentsInChildren<Image>();
		damageImage = GameObject.Find ("Damage Image").GetComponent<Image> ();

		animPlayer = GetComponent < Animator > ();
		playerAudio = GetComponents < AudioSource > ();

		playerMovement = GetComponent < PlayerMovement > ();
		playerShooting = GetComponentInChildren < PlayerShooting > ();
	}

	// Before each level, make sure the player starts out with the max health and the full heart is displayed
	void Start() {
		// Making all of the other hearts that display health below 100 inactive
		// The heart corresponding to 100 heart is left active as the default, 
		// And as health decreases, the hearts pile on top of the preexisting one
		for (int i = Constants.INDEX_HP_90; i < 11; i++) {
			removeHeart (i);
		}

		// Removing the hearts that correspond to lives being lost
		int livesUsed = Constants.STARTINGLIVES - lives; 
		for (int i = Constants.INDEX_HEART_EXTRA1; i < Constants.INDEX_HEART_EXTRA1 + livesUsed; i++) {
			removeHeart (i);
		}

		playerCurrentHealth = startingHealth;
	}
		
	// Used to check if the player is damaged, and flash a light red image onto the screen if so
	void Update() {
		if (damaged) {
			damageImage.color = flashColour;
		} else {
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, Constants.TIME_FLASH_SCREEN * Time.deltaTime);     // Used to slowly fade out the damage image
		}

		damaged = false;
	}

	// Called when the player has been attacked (see EnemyAttack)
	// @param amount- the amount of damage given by the Enemy
	public void TakeDamage(int amount) {
		damaged = true;     // Used to flash the damageImage

		playerCurrentHealth -= amount;     // Updating the health and the corresponding heart
		changeHeart (playerCurrentHealth);

		playerAudio[Constants.INDEX_AUDIO_PLAYER_HURT].Play();     // Playing the "hurt" sound

		if (playerCurrentHealth <= 0 && !isDead) {
			Death();
		}
	}

	// Called when the player's health dips below 0
	void Death() {
		lives--;
		isDead = true;

		playerShooting.DisableEffects();

		animPlayer.SetTrigger("Die");      // Playing the "death" animation and sound
		playerAudio[Constants.INDEX_AUDIO_PLAYER_DEATH].Play();     

		playerMovement.enabled = false;     // Disabling player scripts
		playerShooting.enabled = false;
	}

	// Accessor method to retrieve the player's current health
	public int getCurrentHealth() {
		return playerCurrentHealth;
	}

	// Used to update the heart sprite when damage is taken
	// @param health- the player's current health
	void changeHeart(int health) {
		if (health <= 0) {
			updateHeart (Constants.INDEX_HP_0);
		} else if (health <= .1 * startingHealth) {
			updateHeart (Constants.INDEX_HP_10);
		} else if (health <= .2 * startingHealth) {
			updateHeart (Constants.INDEX_HP_20);
		} else if (health <= .3 * startingHealth) {
			updateHeart (Constants.INDEX_HP_30);
		} else if (health <= .4 * startingHealth) {
			updateHeart (Constants.INDEX_HP_40);
		} else if (health <= .5 * startingHealth) {
			updateHeart (Constants.INDEX_HP_50);
		} else if (health <= .6 * startingHealth) {
			updateHeart (Constants.INDEX_HP_60);
		} else if (health <= .7 * startingHealth) {
			updateHeart (Constants.INDEX_HP_70);
		} else if (health <= .8 * startingHealth) {
			updateHeart (Constants.INDEX_HP_80);
		} else if (health <= .9 * startingHealth) {
			updateHeart (Constants.INDEX_HP_90);
		}
	}

	// Making a heart sprite active
	// @param index- the index of the heart in HealthUI (see Constants.cs)
	void updateHeart(int index) {
		hearts [index].gameObject.SetActive (true);
	}

	// Making a heart sprite inactive
	// @param index- the index of the heart in HealthUI (see Constants.cs)
	void removeHeart(int index) {
		hearts [index].gameObject.SetActive (false);
	}
}