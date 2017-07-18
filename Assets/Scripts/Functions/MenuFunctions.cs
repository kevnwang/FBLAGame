using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Class holding functions that are not part of the Levels
// (Non-gameplay related)
public class MenuFunctions: MonoBehaviour {

	public static int levelCount = 0;     // static variable to keep track of the level that the player is on
	static float menuVolume = Constants.VOLUME_VALUE_DEFAULT;

	// References to the Exit Canvas and its children
	GameObject exitCanvas;
	GameObject quitPanel;
	GameObject menuPanel; 

	Slider volumeSlider;     // Reference to the Menu's volume slider

	/* --------------------------
	 * | Scenes in build | Index |
	 * |-----------------|-------|
	 * | Main Menu       |   0   |
	 * | How To Play     |   1   |
	 * | End Game        |   2   |
	 * | Level 01        |   3   |
	 * | Level 02        |   4   |
	 * | Level 03        |   5   |
	 * | Level 04        |   6   |
	 * | Next Level      |   7   |
	 * | Game Over       |   8   |
	 * ---------------------------
	 * */

	// Array to hold all of the level names
	string[] levelArr = new string[] { 
		"Level 01",
		"Level 02",
		"Level 03",
		"Level 04"
	};

	void Awake() {
		exitCanvas = GameObject.Find ("ExitCanvas");
		quitPanel = GameObject.Find ("QuitPanel");
		menuPanel = GameObject.Find ("MenuPanel");

		volumeSlider = GetComponentInChildren<Slider> ();
	}

	void Start() {
		DontDestroyOnLoad (GameObject.Find ("MenuMusic"));     // Allowing the same music to play across scenes (Main Menu + How To Play)

		if (exitCanvas != null) {
			exitCanvas.SetActive (false);     // Disabling the Exit Canvas, if the scene has one (ex. How To Play does not have one)
		}

		if (SceneManager.GetActiveScene().buildIndex < 3) {
			volumeSlider.value = menuVolume * Constants.SLIDER_VALUE_MAX;     // Used to apply preexisting menu volume for those scenes with menu sound
			AudioListener.volume = menuVolume;
		}
	}

	// Called by the StartButton and ReplayButton's onClickListener
	// Begins/Replays each level
	public void startGame() {
		levelCount++;

		// Tests if the user has completed all of the levels
		// And if done so, he is sent to the celebratory end scene
		// If not, he is sent to the next level
		if (levelCount > levelArr.Length) {
			SceneManager.LoadScene("End Game");
		} else {
			SceneManager.LoadScene(levelArr[levelCount - 1]);      // levelcount - 1 is used because the levelArr starts at index 0
		}
	}

	// Called by the QuitButton's onClickListener
	// Prompts the Exit Canvas
	// Disables the Menu Panel, only allowing the Quit Panel to be shown
	public void quitGame() {
		exitCanvas.SetActive (true);
		menuPanel.SetActive (false);
	}

	// Called by the MainMenuButton's onClickListener
	// Prompts the Exit Canvas
	// Disables the Quit Panel, only allowing the Menu Panel to be shown
	public void exitToMainMenu() {
		exitCanvas.SetActive (true);
		quitPanel.SetActive (false);
	}

	// Called by the HowToPlayButton's onClickListener
	public void HowToPlay() {
		SceneManager.LoadScene ("How To Play");
	}

	// Confirming "Quit Game"
	public void exitYes() {
		Application.Quit ();
	}

	// Refuting "Quit Game", closing the Exit Canvas and reenabling the Menu Panel for further use
	public void exitNo() {
		menuPanel.SetActive (true);
		exitCanvas.SetActive (false);
	}

	// Confirming "Exit To Main Menu"
	public void menuYes() {
		levelCount = 0;     // Exiting to the Main Menu restarts you at the first level

		DataManager.TOTAL_ZOMBUNNY = 0;    // Resets the kills and score
		DataManager.TOTAL_ZOMBEAR = 0;
		DataManager.TOTAL_HELLEPHANT = 0;
		DataManager.TOTAL_SCORE = 0;

		PlayerHealth.lives = Constants.STARTINGLIVES;     // Resets lives

		SceneManager.LoadScene("Main Menu");
	}

	// Refuting "Exit TO Main Menu", closing the Exit Canvas and reenabling the Quit Panel for further use
	public void menuNo() {
		quitPanel.SetActive (true);
		exitCanvas.SetActive (false);
	}

	public void changeVolume() {
		menuVolume = volumeSlider.value / Constants.SLIDER_VALUE_DEFAULT * Constants.VOLUME_VALUE_DEFAULT;
		AudioListener.volume = menuVolume;
	}
}