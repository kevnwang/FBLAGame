using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

// Class overseeing functions in the "NextLevel" scene
// in which the user must add up all the points he/she
// accumulated and enter a correct answer to go on to
// the next level
public class EducationalManager: MonoBehaviour {

	/* Multidimensional array to hold all of the pulled data:
	 * -------------------------------------
	 * | zombunnyKills   | zombunnyWorth   |
	 * | zombearKills    | zombearWorth    |
	 * | hellephantKills | hellephantWorth |
	 * -------------------------------------
	 * */
	int[, ] enemiesReceive;

	Text[] nextLevelTexts;
	Animator animNextLevel;      // Reference to the NextLevel Animator Controller 

	void Awake() {
		enemiesReceive = GameObject.Find("TransferredData").GetComponent < DataManager > ().getEnemies();     // The transferred data from the previous level being pulled
		nextLevelTexts = GetComponentsInChildren < Text > ();
		animNextLevel = GetComponentInParent < Animator > ();
	}

	// Used to display the data to the user for calculation at the start of the scene
	void Start() {
		nextLevelTexts[Constants.INDEX_NEXT_TXT_SCORE_DISP].text = "You killed: \n"

			+ enemiesReceive[0, 0] + " Zombunnies, " 
			+ enemiesReceive[1, 0] + " Zombears, " 
			+ enemiesReceive[2, 0] + " Hellephants. \n"

			+ "\n If Zombunnies are worth " + enemiesReceive[0, 1] + " points," 
			+ "\n Zombears are worth " + enemiesReceive[1, 1] + " points," 
			+ "\n and Hellephants are worth " + enemiesReceive[2, 1] + " points,"

			+ "\n \n How many points did you score?"

			+ "\n (Click the box to enter your answer)";
		
		if (!GameFunctions.musicOn) {
			AudioListener.volume = 0;
		}
	}

	// Called by the ContinueButton's onClickListener
	public void checkAnswer() {
		// Adds up the total score
		int score = enemiesReceive[0, 0] * enemiesReceive[0, 1] +
			enemiesReceive[1, 0] * enemiesReceive[1, 1] +
			enemiesReceive[2, 0] * enemiesReceive[2, 1];

		// Checks if the user entered the right score
		if (nextLevelTexts[Constants.INDEX_NEXT_TXT_SCORE_INPUT].text.Equals(score.ToString())) {
			nextLevelTexts[Constants.INDEX_NEXT_TXT_NOTIF].text = "Correct!";
			animNextLevel.SetTrigger("AnswerCorrect");      // Also calls StartGame (see MenuFunctions)
		} else {
			nextLevelTexts[Constants.INDEX_NEXT_TXT_NOTIF].text = "Incorrect";
			animNextLevel.SetTrigger("AnswerIncorrect");
		}
	}
}