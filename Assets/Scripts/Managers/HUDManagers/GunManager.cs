using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Class to control what gun is being displayed on the HUD
public class GunManager: MonoBehaviour {

	PlayerGun playerGun;
	Text gunText;

	void Awake() {
		playerGun = GameObject.FindGameObjectWithTag("Player").GetComponent < PlayerGun > ();
		gunText = GetComponent < Text > ();
	}

	void Update() {
		gunText.text = playerGun.getFullName();
	}
}