using UnityEngine;

// Class used to control the spawning(instantiation) of enemies
public class EnemyManager: MonoBehaviour {
	
	public GameObject[] enemies;     // Array of GameObjects holding the enemies that can be spawned
	public float enemySpawnTime = 3.0f;     // Time between individual spawns
	public Transform[] enemySpawnPoints;     // Array of various location the enemies can be spawned at

	int numEnemies;      // private variable to keep track of the current number of enemies that are alive
	PlayerHealth playerHealth;

	void Awake() {
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent < PlayerHealth > ();
	}

	void Start() {
		numEnemies = 0;     // At the start of each level, reset the number of enemies that have been instantiated
		InvokeRepeating("Spawn", enemySpawnTime, enemySpawnTime);     // Start the spawning process- call the method "Spawn" every (spawnTime) seconds
	}

	void Spawn() {
		if (playerHealth.getCurrentHealth() <= 0f || numEnemies >= Constants.MAX_NUMENEMIES) {
			return;     // Don't spawn any enemies if the player is dead or there is already the max number of enemies alive
		}

		// Code to decide which enemy to spawn
		// The probability to spawn a zombunny is 50%
		// The probability to spawn a zombear is 30%
		// The probability to spawn a hellephant is 20%
		int num = Random.Range(1, 10);
		int enemyIndex;
		if (num < 5) {
			enemyIndex = Constants.INDEX_ENEMY_ZOMBUNNY;
		} else if (num < 8) {
			enemyIndex = Constants.INDEX_ENEMY_ZOMBEAR;
		} else {
			enemyIndex = Constants.INDEX_ENEMY_HELLEPHANT;
		}

		// Spawn the enemy and add it to the enemy count
		Instantiate(enemies[enemyIndex], enemySpawnPoints[enemyIndex].position, enemySpawnPoints[enemyIndex].rotation);
		numEnemies++;
	}

	// Called when an enemy is killed
	// Updates the current count of enemies alive
	public void addKill() {
		numEnemies--;
	}
}