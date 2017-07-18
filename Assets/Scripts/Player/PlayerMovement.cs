using UnityEngine;

// Class used to control the player's movement
public class PlayerMovement: MonoBehaviour {
	public float speed = 6f;

	// Component references
	int floorMask;     // Reference to the floor layer
	Animator animChar;      // Reference to the player's Animator Controller
	Animator animHUD;      // Reference to the HUD's Animator Controller
	Rigidbody playerRigidbody;
	AudioSource[] playerAudio;     // Array of sounds that the player can make during the game

	// Script references
	PlayerGun playerGun;
	PlayerShooting  playerShooting;

	Vector3 movement;

	void Awake() {
		floorMask = LayerMask.GetMask("Floor");
		animChar = GetComponent < Animator > ();
		animHUD = GameObject.Find ("HUDCanvas").GetComponent < Animator > ();
		playerRigidbody = GetComponent < Rigidbody > ();
		playerAudio = GetComponents < AudioSource > ();

		playerGun = GetComponent < PlayerGun > ();
		playerShooting = GetComponentInChildren<PlayerShooting> ();
	}

	// Used to control the player's RigidBody movement
	void FixedUpdate() {
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		Move(horizontal, vertical);
		Turning();
		Animating(horizontal, vertical);
	}

	// Used to move the player based on user input (WASD or Arrow keys)
	void Move(float h, float v) {
		movement.Set(h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime;

		playerRigidbody.MovePosition(transform.position + movement);
	}

	// Used to turn the player based on mouse position
	void Turning() {
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit floorHit;
		if (Physics.Raycast(camRay, out floorHit, Constants.LENGTH_CAMRAY, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;     // Make a vector from the player to the location of the mouse
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	// Used to animate the player's walking
	void Animating(float h, float v) {
		bool walking = h != 0f || v != 0f;
		animChar.SetBool("IsWalking", walking);
	}

	// Used to test whether the player has encountered a pickup
	void OnTriggerEnter(Collider other) {
		if (!other.gameObject.CompareTag("Enemy")) {
			other.gameObject.SetActive(false);     // Make the pickup disappear

			// Encountering an AmmoDrop
			if (other.gameObject.CompareTag("AmmoDrop")) {
				playerAudio[Constants.INDEX_AUDIO_PICKUP_AMMO].Play();
				animHUD.SetTrigger ("AmmoPicked");
				PickUpManager.ammoPicked = false;
				playerGun.addAmmo();
			} else if (other.gameObject.CompareTag("GunDrop")) {     // Encountering a GunDrop
				playerAudio[Constants.INDEX_AUDIO_PICKUP_GUN].Play();
				animHUD.SetTrigger ("NewWeapon");
				PickUpManager.gunPicked = false;
				Destroy (GameObject.FindGameObjectWithTag ("AmmoDrop"));     // The new weapon contains full ammo, so destroy any AmmoDrops that may have been instantiated
				PickUpManager.ammoPicked = false;
				playerShooting.stopReloading ();     // Used to cancel any reloading sounds
				playerGun.setWeapon (Constants.parseName(other.gameObject.name));     // Replace the player's current gun with the new gun
			}
		}
	}
}