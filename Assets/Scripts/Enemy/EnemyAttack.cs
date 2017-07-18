using UnityEngine;
using System.Collections;

// Class to control each enemy's attack
// Is a component of every instantiated enemy 
public class EnemyAttack: MonoBehaviour {
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;

	Animator animEnemy; // Reference to the enemy Animator Controller: "EnemyAC"
	GameObject player; 

	// Script references
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth; 

	bool playerInRange;
	float attackTimer;

	// Awake is called when the script is being loaded
	// Used to initialize variables
	void Awake() {
		animEnemy = GetComponent < Animator > ();
		player = GameObject.FindGameObjectWithTag("Player");

		playerHealth = player.GetComponent < PlayerHealth > ();
		enemyHealth = GetComponent < EnemyHealth > ();
	}

	// OnTriggerEnter called when Colliders meet
	// Used to check if the enemy should attack the designated GameObject- Player
	void OnTriggerEnter(Collider other) {
		if (other.gameObject == player) {
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == player) {
			playerInRange = false;
		}
	}

	// Update is called once per frame
	// Used to control the enemy's attack rate
	void Update() {
		attackTimer += Time.deltaTime;

		// Requirements to attack: 
		// Enough time has elasped for the next attack AND
		// The player is in range to be attacked AND
		// The enemy has health
		if (attackTimer >= timeBetweenAttacks && playerInRange && enemyHealth.getEnemyCurrentHealth() > 0) {
			Attack();
		}

		if (playerHealth.getCurrentHealth() <= 0) {
			animEnemy.SetTrigger("PlayerDead");     // This animation sets all enemies to idle, instead of walking
		}
	}

	void Attack() {
		attackTimer = 0f;

		if (playerHealth.getCurrentHealth() > 0) {
			playerHealth.TakeDamage(attackDamage);
		}
	}
}