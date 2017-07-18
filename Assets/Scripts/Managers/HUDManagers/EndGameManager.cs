using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Class used to control the end game display
public class EndGameManager : MonoBehaviour {

	Text[] endTexts;     // Array of all the texts in the End Game Canvas

	float duration = 1f;
	float lerpTimer = 0f;

	void Awake() {
		endTexts = GetComponentsInChildren<Text> ();
	}

	// Used to display the final score
	void Start() {
		endTexts[Constants.INDEX_END_SCORE].text = "You killed a total of: \n"
							+ DataManager.TOTAL_ZOMBUNNY + " Zombunnies, "
							+ DataManager.TOTAL_ZOMBEAR + " Zombears, "
							+ DataManager.TOTAL_HELLEPHANT + " Hellephants,"
							+ "\n For a total score of " + DataManager.TOTAL_SCORE + "!";
	}

	// Used to animate the changing color of the banner text (using Color.Lerp)
	void Update () {
		endTexts[Constants.INDEX_END_BANNER].color = Color.Lerp (Color.blue, Color.white, lerpTimer);

		// Looping the animation
		if (lerpTimer < 1) {
			lerpTimer += Time.deltaTime / duration;
		} else {
			lerpTimer = 0;
		}
	}
}
