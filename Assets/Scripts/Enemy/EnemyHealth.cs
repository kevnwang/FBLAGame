using UnityEngine;

// Class to monitor each enemy's health
// Is a component of every instantiated enemy
public class EnemyHealth: MonoBehaviour {

	// Public variables that can be changed in the Unity application
	// Used to modify different types of enemies
	public int startingHealth = 100;
	public AudioClip deathClip;
	public string enemyType;

	// Enemy attributes
	float sinkSpeed = 2.5f;
	int currentHealth;
	bool isDead;
	bool isSinking;

	// Component references
	Animator animEnemy;
	AudioSource enemyAudio;
	ParticleSystem hitParticles;
	CapsuleCollider capsuleCollider;

	// Script references
	ScoreManager scoreManager;
	EnemyManager enemyManager;

	void Awake() {
		animEnemy = GetComponent < Animator > ();
		enemyAudio = GetComponent < AudioSource > ();
		hitParticles = GetComponentInChildren < ParticleSystem > ();
		capsuleCollider = GetComponent < CapsuleCollider > ();
		currentHealth = startingHealth;

		scoreManager = GameObject.Find("Score").GetComponent < ScoreManager > ();
		enemyManager = GameObject.Find("EnemyManager").GetComponent < EnemyManager > ();

		enemyType = Constants.parseName(GetComponent < Transform > ().name); 
	}

	// Used to animate the enemy disappearing into the ground when dying
	void Update() {
		if (isSinking) {
			transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

	/*
	 * @desc TakeDamage is called when the enemy is shot by the Player
	 * @param int amount- the damage taken
	 * @param Vector3 hitPoint- the location shot
	 * */
	public void TakeDamage(int amount, Vector3 hitPoint) {
		if (isDead) {
			return;
		}

		enemyAudio.Play();

		currentHealth -= amount;

		hitParticles.transform.position = hitPoint;
		hitParticles.Play();

		if (currentHealth <= 0) {
			Death();
		}
	}

	// Death is called when the enemy's health dips below 0
	void Death() {
		isDead = true;

		capsuleCollider.isTrigger = true;

		animEnemy.SetTrigger("Dead");     // This animation also calls the StartSinking method

		enemyAudio.clip = deathClip;
		enemyAudio.Play();

		// The enemy's death is recorded
		scoreManager.addScore(enemyType);    
		enemyManager.addKill();
	}

	// StartSinking is called when the enemy dies
	public void StartSinking() {
		GetComponent < UnityEngine.AI.NavMeshAgent > ().enabled = false;
		GetComponent < Rigidbody > ().isKinematic = true;     // Setting the Rigidbody to "isKinematic" disables physics(Forces, collisions, etc.)
		isSinking = true;
		Destroy(gameObject, 2f);
	}

	// Accessor method for other enemy scripts
	public int getEnemyCurrentHealth() {
		return currentHealth;
	}
}