using UnityEngine;

// Class that holds attributes of the different guns 
public class PlayerGun: MonoBehaviour {
	public GameObject startingGun;     // To allow the changing of the starting weapon from the Unity application
	public Material[] gunMaterials;     // Array of gun materials that can correspond to the different guns

	// private variables that represent different attributes of each type of gun
	string fullName;
	int startingAmmo;
	int magazineSize;
	float reloadDuration;
	int damage;
	float timeBetweenBullets;
	float range;

	int currentAmmo;
	int reserveAmmo;

	AudioSource[] gunFireSounds;     // Array of gun sounds that correspond to the different guns
	SkinnedMeshRenderer gunRenderer;     // Reference to the SkinnedMeshRenderer of the gun

	string input;     // The gun's shortened name
	int audioIndex;     // Used to determine which gun sound to use

	public static bool isReloading;    

	void Awake() {
		gunFireSounds = GameObject.Find("GunBarrelEnd").GetComponents < AudioSource > ();
		gunRenderer = GetComponentsInChildren<SkinnedMeshRenderer> ()[Constants.INDEX_REND_GUN];
	}

	void Start() {
		setWeapon(startingGun.gameObject.name);
		gunRenderer.enabled = true;
	}

	// Called when the player picks up a new weapon (see PlayerMovement)
	public void setWeapon(string input) {
		this.input = input;
		isReloading = false;
		int materialIndex;     // Used to determine which material to apply

		// Each gun has a different name, magazine size, etc.
		switch (input) {
		case "SMG":
			fullName = "Submachine Gun";
			magazineSize = 40;
			reloadDuration = 2.0f;
			damage = 25;
			timeBetweenBullets = 0.13f;
			range = 50f;
			audioIndex = Constants.INDEX_AUDIO_SMG;
			materialIndex = Constants.INDEX_MATERIAL_SMG;
			break;
		case "Shotgun":
			fullName = "Shotgun";
			magazineSize = 8;
			reloadDuration = 3.0f;
			damage = 100;
			timeBetweenBullets = 1.2f;
			range = 10f;
			audioIndex = Constants.INDEX_AUDIO_SHOTGUN;
			materialIndex = Constants.INDEX_MATERIAL_SHOTGUN;
			break;
		case "LMG":
			fullName = "Light Machine Gun";
			magazineSize = 75;
			reloadDuration = 8.5f;
			damage = 50;
			timeBetweenBullets = 0.4f;
			range = 100f;
			audioIndex = Constants.INDEX_AUDIO_LMG;
			materialIndex = Constants.INDEX_MATERIAL_LMG;
			break;
		case "Sniper":
			fullName = "Sniper Rifle";
			magazineSize = 5;
			reloadDuration = 3.0f;
			damage = 300;
			timeBetweenBullets = 1.5f;
			range = 1000f;
			audioIndex = Constants.INDEX_AUDIO_SNIPER;
			materialIndex = Constants.INDEX_MATERIAL_SNIPER;
			break;
		case "AssaultRifle":
			fullName = "Assault Rifle";
			magazineSize = 30;
			reloadDuration = 2.5f;
			damage = 35;
			timeBetweenBullets = 0.15f;
			range = 100f;
			audioIndex = Constants.INDEX_AUDIO_ASSAULTRIFLE;
			materialIndex = Constants.INDEX_MATERIAL_ASSAULTRIFLE;
			break;
		default:
			fullName = input;
			magazineSize = 0;
			reloadDuration = 0f;
			damage = 0;
			timeBetweenBullets = 0f;
			range = 0f;
			audioIndex = Constants.INDEX_AUDIO_ASSAULTRIFLE;
			materialIndex = Constants.INDEX_MATERIAL_ASSAULTRIFLE;
			break;
		}

		// Applying changes
		startingAmmo = magazineSize * Constants.AMMO_MULTIPLIER;
		currentAmmo = magazineSize;
		reserveAmmo = startingAmmo - currentAmmo;
		gunRenderer.material = gunMaterials [materialIndex];
	}

	// Called when a bullet is fired (see PlayerShooting)
	public void Fire() {
		gunFireSounds[audioIndex].Play();
		currentAmmo--;
	}

	// Called at the end of reloading (see PlayerShooting)
	// Used to provide the player with more ammo from the player's reserve ammo
	public void ChangeAmmo() {
		int ammoUsed = magazineSize - currentAmmo;

		// If not enough reserve ammo is availabe for a full magazine,
		// just refill as much as possible
		if (reserveAmmo < ammoUsed) {
			currentAmmo += reserveAmmo;
			reserveAmmo = 0;
		} else {
			reserveAmmo -= ammoUsed;
			currentAmmo = magazineSize;
		}
	}

	// Mutator method called when the player picks up more ammo (See PlayerMovement)
	public void addAmmo() {
		reserveAmmo += magazineSize * 2;
	}

	// Accessor methods to retrieve information about the current gun or ammo (See PlayerShooting, AmmoManager)

	// getFullName is used to display the name of the gun on the HUD
	public string getFullName() {
		return fullName;
	}

	// getShortenedName is used to test equality for gun names (See PickUpManager)
	public string getShortenedName() {
		return input;
	}

	public int getAmmo() {
		return currentAmmo;
	}
		
	public int getReserveAmmo() {
		return reserveAmmo;
	}

	public int getMagazineSize() {
		return magazineSize;
	}

	public float getReloadDuration() {
		return reloadDuration;
	}

	public int getDamage() {
		return damage;
	}

	public float getFireRate() {
		return timeBetweenBullets;
	}

	public float getRange() {
		return range;
	}
}