using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

// Class to control the lives remaining
public class LivesManager: MonoBehaviour {

	Text livesText;

	void Awake() {
		livesText = GetComponent < Text > ();

		// Checking if the player has run out of lives,
		// disable the replay button
		if (PlayerHealth.lives <= 0) {
			GameObject.Find("ReplayButton").SetActive(false);
		}
	}

	void Update() {
		livesText.text = "Lives Remaining: " + PlayerHealth.lives;

		if (PlayerHealth.lives <= 1) {
			livesText.color = Color.red;     // Indication that the player is low on lives
		} else {
			livesText.color = Color.white;
		}
	}
}