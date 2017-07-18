using UnityEngine;
using UnityEngine.UI;

// Class to monitor the player's shooting behavior
public class PlayerShooting: MonoBehaviour {
	Ray shootRay;
	RaycastHit shootHit;

	// Component references
	int shootableMask;     // Reference to the shootable layer
	ParticleSystem gunParticles;
	LineRenderer gunLine;
	AudioSource[] gunAudio;     // Reference to the array of gun sounds 
	Light gunLight;
	PlayerGun playerGun;

	float reloadTimer;
	float shootingTimer;

	void Awake() {
		shootableMask = LayerMask.GetMask("Shootable");
		gunParticles = GetComponent < ParticleSystem > ();
		gunLine = GetComponent < LineRenderer > ();
		gunAudio = GetComponents < AudioSource > ();
		gunLight = GetComponent < Light > ();
		playerGun = GetComponentInParent < PlayerGun > ();
	}
		
	void Update() {
		shootingTimer += Time.deltaTime;

		if (playerGun.getReserveAmmo() > 0 || playerGun.getAmmo() > 0) {     // The gun has ammo:
			/* Requirements to reload:
			 * The player is still reloading OR
			 * The player has emptied his magazine OR
			 * The player is trying to reload a partially filled clip, with some reserve ammo available
			 */
			if ((PlayerGun.isReloading || playerGun.getAmmo() <= 0 || Input.GetKeyDown("r") && playerGun.getAmmo() < playerGun.getMagazineSize()) && playerGun.getReserveAmmo() > 0) {
				keepReloading();
			} 
			/* Requirements to shoot:
			 * The player has met the opposite of the reloading requirements above AND
			 * The player is trying to shoot AND
			 * Enough time has elasped so the next bullet can be fired AND
			 * The game is not paused
			 */
			else if (Input.GetButton("Fire1") && shootingTimer >= playerGun.getFireRate() && Time.timeScale != 0) {
				reloadTimer = 0f;
				Shoot();
			}
		} else if (Input.GetButton("Fire1")) {    // The gun has no ammo:
			gunAudio[Constants.INDEX_AUDIO_NOAMMO].Play();
		}

		// Controls the amount of the time gun shows effects
		if (shootingTimer >= playerGun.getFireRate() * Constants.TIME_FLASH_MUZZLE) {
			DisableEffects();
		}
	}

	public void DisableEffects() {
		gunLine.enabled = false;
		gunLight.enabled = false;
	}

	// Called when a bullet is being fired
	void Shoot() {
		shootingTimer = 0f;

		gunLight.enabled = true;

		gunParticles.Stop();
		gunParticles.Play();

		gunLine.enabled = true;
		gunLine.SetPosition(0, transform.position);

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		// Gun ray is stopped at the first "shootable" (denoted by layer) object, either an object in the environment or an enemy
		if (Physics.Raycast(shootRay, out shootHit, playerGun.getRange(), shootableMask)) {
			EnemyHealth enemyHealth = shootHit.collider.GetComponent < EnemyHealth > ();

			// Testing if the bullet hit an enemy
			if (enemyHealth != null) {
				enemyHealth.TakeDamage(playerGun.getDamage(), shootHit.point);
			}

			gunLine.SetPosition(1, shootHit.point);
		} else {     // Gun ray is extended throughout the gun's range
			gunLine.SetPosition(1, shootRay.origin + shootRay.direction * playerGun.getRange());
		}

		playerGun.Fire();
	}

	// Used to continue reloading until the reload duration has been reached
	void keepReloading() {
		// Actions at the beginning of the reload:
		// Start the reload sound(in a loop) and set the ammo slider to 0
		if (!(PlayerGun.isReloading)) {
			gunAudio[Constants.INDEX_AUDIO_RELOADING].Play();
			GameObject.Find ("AmmoSlider").GetComponent<Slider> ().value = 0;
		}

		PlayerGun.isReloading = true;
		reloadTimer += Time.deltaTime;

		// Actions at the end of the reload:
		// Refill the ammo and stop the reload sound
		if (reloadTimer >= playerGun.getReloadDuration()) {
			PlayerGun.isReloading = false;
			playerGun.ChangeAmmo();
			gunAudio [Constants.INDEX_AUDIO_RELOADING].Stop ();
		}
	}

	public void stopReloading() {
		gunAudio [Constants.INDEX_AUDIO_RELOADING].Stop ();
	}
}