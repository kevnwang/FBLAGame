using UnityEngine;
using System.Collections;

// Class to control AI enemy movement through Unity's NavMeshAgent
// Is a component of every instantiated enemy
public class EnemyMovement: MonoBehaviour {
	Transform player;     // Reference to the player's position

	// Script references
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;

	UnityEngine.AI.NavMeshAgent nav;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").transform;

		playerHealth = player.GetComponent < PlayerHealth > ();
		enemyHealth = GetComponent < EnemyHealth > ();

		nav = GetComponent < UnityEngine.AI.NavMeshAgent > ();
	}

	void Update() {
		// Requirements to move:
		// Enemy has health AND
		// Player has health
		if (enemyHealth.getEnemyCurrentHealth() > 0 && playerHealth.getCurrentHealth() > 0) {
			nav.SetDestination(player.position);
		} else {
			nav.enabled = false;
		}
	}
}