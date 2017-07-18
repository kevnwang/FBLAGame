using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Class to control the score being accumulated
public class ScoreManager: MonoBehaviour {

	// public variables that hold the amount of points each type of enemy is worth
	public int zombunnyWorth;
	public int zombearWorth;
	public int hellephantWorth;

	// private counts of enemies killed
	int totalKills;
	int zombunnyKills;
	int zombearKills;
	int hellephantKills;

	GameFunctions gameFunctions;
	Text[] scoreTexts;

	void Awake() {
		gameFunctions = GetComponentInParent < GameFunctions > ();
		scoreTexts = GetComponentsInChildren<Text> ();
	}

	void Start() {
		// At the start of each level, reset the counts of enemies killed
		zombunnyKills = 0;
		zombearKills = 0;
		hellephantKills = 0;
	}

	// Used to update the counts of enemies killed on the HUD
	void Update() {
		scoreTexts [Constants.INDEX_ENEMY_ZOMBUNNY].text = zombunnyKills.ToString ();
		scoreTexts [Constants.INDEX_ENEMY_ZOMBEAR].text = zombearKills.ToString ();
		scoreTexts [Constants.INDEX_ENEMY_HELLEPHANT].text = hellephantKills.ToString ();

		totalKills = zombunnyKills + zombearKills + hellephantKills;

		if (gameFunctions.levelPassed()) {
			scoreTexts[Constants.INDEX_ENEMY_ZOMBUNNY].text += "\n \n Level Passed!";     // Indication that the player has passed the level
		}
	}

	// Mutator method called when the enemy dies
	// to update the kill counters of each enemy type
	// @param string type- the type of enemy to be counted
	public void addScore(string type) {
		switch (type) {
		case "Zombunny":
			zombunnyKills++;
			break;
		case "ZomBear":
			zombearKills++;
			break;
		case "Hellephant":
			hellephantKills++;
			break;
		}
	}

	// Accessor methods for the DataManager to pass the scores onto
	// the educational scene

	public int getTotalKills() {
		return totalKills;
	}

	public int getZombunnyKills() {
		return zombunnyKills;
	}

	public int getZombearKills() {
		return zombearKills;
	}

	public int getHellephantKills() {
		return hellephantKills;
	}
}