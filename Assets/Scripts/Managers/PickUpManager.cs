using UnityEngine;
using System.Collections;

// Class used to manage the spawning of new weapons and ammo
public class PickUpManager: MonoBehaviour {
	/* Array of GameObjects holding the pickups that can be spawned
	 * -------------------------
	 * | Pickup        | Index |
	 * -------------------------
	 * | Ammo          |   0   |
	 * | SMG (Gun)     |   1   |
	 * | Shotgun       |   2   |
	 * | LMG (Gun)     |   3   |
	 * | Sniper        |   4   |
	 * | Assault Rifle |   5   |
	 * -------------------------
	 * */
	public GameObject[] Pickups;     

	/* Array of various locations the pickups can be spawned
	 * --------------------------------
	 * | Location             | Index |
	 * --------------------------------
	 * | WeaponSpawnPoint 1   |   0   |
	 * | WeaponSpawnPoint 2   |   1   |
	 * | WeaponSpawnPoint 3   |   2   |
	 * | AmmoSpawnPoint 1     |   3   |
	 * | AmmoSpawnPoint 2     |   4   |
	 * | AmmoSpawnPoint 3     |   5   |
	 * --------------------------------
	 * */
	public Transform[] pickUpSpawnPoints;     

	public float ammoDelay = 15.0f;     // Delay before spawning
	public float weaponDelay = 15.0f;

	PlayerGun playerGun;

	// static variables to decide when to spawn a pickup (see PlayerMovement)
	public static bool ammoPicked;
	public static bool gunPicked;

	void Awake() {
		playerGun = GameObject.FindGameObjectWithTag("Player").GetComponent < PlayerGun > ();
	}

	// reset variables at the start of the level
	void Start() {
		ammoPicked = false;
		gunPicked = false;
	}

	// Used to determine when to spawn a pickup
	void Update() {
		// Requirements to spawn ammo:
		// If the player's reserve ammo gets low AND
		// There is no ammo already spawned
		if (playerGun.getReserveAmmo() <= playerGun.getMagazineSize() && !ammoPicked) {
			ammoPicked = true;
			Invoke("spawnAmmo", ammoDelay);
		}

		// If there is no gun already spawned, spawn a new gun
		if (!gunPicked) {
			gunPicked = true;
			Invoke("spawnWeapon", weaponDelay);
		}
	}

	// Picks a random ammo spawn location and spawns an "AmmoDrop" there
	void spawnAmmo() {
		int spawnPointIndex = Random.Range(Constants.INDEX_SPAWN_AMMO1, Constants.INDEX_SPAWN_AMMO3);
		Instantiate(Pickups[Constants.INDEX_PICKUP_AMMO], pickUpSpawnPoints[spawnPointIndex].position, pickUpSpawnPoints[spawnPointIndex].rotation);
	}

	// Picks a random weapon and a random weapon spawn location and spawns the wepaon there
	void spawnWeapon() {
		int spawnPointIndex = Random.Range(Constants.INDEX_SPAWN_WEAPON1, Constants.INDEX_SPAWN_WEAPON3);

		int gunIndex;
		do {
			gunIndex = Random.Range(Constants.INDEX_PICKUP_SMG, Constants.INDEX_PICKUP_ASSAULTRIFLE);
		}
		while (Pickups[gunIndex].name.Equals(playerGun.getShortenedName()));     // Gun must not be the weapon the player is currently using

		Instantiate(Pickups[gunIndex], pickUpSpawnPoints[spawnPointIndex].position, pickUpSpawnPoints[spawnPointIndex].rotation);
	}
}