using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Class to control the AmmoUI
// consisting of the AmmoCount and the AmmoSlider
public class AmmoManager: MonoBehaviour {
	PlayerGun playerGun;

	// UI references
	Text ammoText;
	Slider ammoSlider;
	Image sliderFill;

	void Awake() {
		playerGun = GameObject.FindGameObjectWithTag("Player").GetComponent < PlayerGun > ();
		ammoText = GetComponentsInChildren < Text > ()[Constants.INDEX_AMMOUI_TXT_COUNT];
		ammoSlider = GetComponentInChildren < Slider > ();
		sliderFill = GetComponentsInChildren < Image > () [Constants.INDEX_AMMOUI_IMG_FILL];
	}

	// Used to monitor two instances of ammo
	// Ammo being used and ammo being reloaded
	void Update() {
		if (PlayerGun.isReloading) {
			ammoText.text = ".  .  .";
			ammoText.color = Color.red;

			/* Code used to fill the ammoSlider while reloading, to simulate the time it takes to reload-
			 * Update is only called once per frame, so the ammo slider needs to be filled according 
			 * to time it took for the last frame to process (Time.deltaTime),
			 * and also according to the reload duration of the gun
			 * 
			 * Calculation: Need to fill 100 increments(of slider) in the given ReloadDuration(in seconds)
			 * 
			 * 100 increments                                  100 increments               IncrementToAdd
			 * -------------------------------------------- =  -------------------------- = --------------
			 * ReloadDuration(seconds) *  FramesProcessed       ReloadDuration(in frames)      1 frame
			 *                            ---------------
			 *                             1 second
			 * */
			float FRAMES_PER_SECOND = 1.0f / Time.deltaTime;
			ammoSlider.value += Constants.SLIDER_VALUE_MAX / (playerGun.getReloadDuration () * FRAMES_PER_SECOND);

			sliderFill.color = Color.yellow;
		} else {
			ammoText.text = playerGun.getAmmo () + "/" + playerGun.getReserveAmmo ();

			// Code used to display the ammo remaining via the slider
			ammoSlider.value = (float)playerGun.getAmmo () / (float)playerGun.getMagazineSize () * Constants.SLIDER_VALUE_MAX;
			sliderFill.color = Color.cyan;

			if (playerGun.getAmmo () <= playerGun.getMagazineSize () / Constants.AMMO_LOWDIVISOR) {
				ammoText.color = Color.red;     // Indication that the player is low on ammo
			} else {
				ammoText.color = Color.white;
			}
		}
	}
}