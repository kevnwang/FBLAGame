using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Class holding various Level-related functions
// "Gameplay related"
public class GameFunctions: MonoBehaviour {
	// Canvas references
	GameObject pauseCanvas;     
	GameObject HTPCanvas;

	Animator animHUD;     // Reference to the HUD (Heads Up Display) Animator Controller

	// Script References
	PlayerHealth playerHealth;    
	ScoreManager scoreManager;
	ObjectiveManager objectiveManager;
	DataManager dataManager;

	// UI References
	AudioSource backgroundMusic;    
	Slider volumeSlider;
	Toggle musicToggle;

	bool isPaused = false;

	// Static variables holding user input values that persist throughout the game
	public static bool musicOn = true;
	static float gameVolume = Constants.VOLUME_VALUE_DEFAULT;

	void Awake() {
		pauseCanvas = GameObject.Find ("PauseCanvas");
		HTPCanvas = GameObject.Find ("HTPCanvas");

		animHUD = GetComponent<Animator> ();

		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth> ();
		scoreManager = GetComponentInChildren<ScoreManager> ();
		objectiveManager = GetComponentInChildren<ObjectiveManager> ();
		// Data is used to transfer enemy information from each "Level 0x" scene to the "Next Level" scene
		// without the data being destroyed between scenes
		// This is accomplished by creating an empty GameObject (TransferredData),
		// placing a script (DataManager) as a component,
		// And calling DontDestroyOnLoad on the GameObject
		dataManager = GameObject.Find("TransferredData").GetComponent<DataManager> ();
		GameObject.DontDestroyOnLoad(dataManager);

		backgroundMusic = GetComponentInChildren<AudioSource> ();
		volumeSlider = pauseCanvas.GetComponentInChildren<Slider> ();
		musicToggle = pauseCanvas.GetComponentInChildren<Toggle> ();
	}

	// Used to start the game with the introductory animation
	void Start() {
		Time.timeScale = 1;
		animHUD.SetTrigger("Intro");     // Introductory animation

		if (GameObject.Find ("MenuMusic") != null) {
			Destroy (GameObject.Find ("MenuMusic"));     // Stop the music playing from the menu screen, if it is playing
		}

		// Disabling other canvases
		pauseCanvas.SetActive (false);     
		HTPCanvas.SetActive (false);
		GameObject.Find ("ExitCanvas").SetActive (false);

		// Setting option preferences
		AudioListener.volume = gameVolume;
		if (!musicOn) {
			backgroundMusic.Stop ();
		}
	}

	// Used to check if the game wishes to be paused or the level is completed
	void Update() {
		if (Input.GetKeyDown("escape")) {
			if (!isPaused) {
				Pause ();
			} else if (isPaused && !HTPCanvas.activeSelf) {     // Cannot unpause if currently looking at the How To Play screen
				unPause ();
			}
		}

		if (levelPassed()) {
			animHUD.SetTrigger("LevelPassed");      // Animation also calls nextLevel
			GetComponentsInChildren<Text>()[Constants.INDEX_HUD_TXT_OBJECTIVE].text += "\nLevel Passed!";     // Editing the objective text
		} else if (playerHealth.getCurrentHealth() <= 0) {
			animHUD.SetTrigger("GameOver");     // Animation also calls gameOver
		}
	}

	// Used to check if the required number of kills meets the objective
	public bool levelPassed() {
		if (scoreManager.getTotalKills() >= objectiveManager.killGoal) {
			return true;
		} else {
			return false;
		}
	}

	// Called by the GameOver animation
	public void gameOver() {
		MenuFunctions.levelCount--;
		SceneManager.LoadScene("Game Over");
	}
		
	// Called by the LevelPassed animation
	// Captures the data (enemies killed, the amount of points each enemy was worth)
	// and sends it to the next scene- "Next Level"
	public void nextLevel() {
		dataManager.captureData(scoreManager.getZombunnyKills(),
			scoreManager.getZombearKills(),
			scoreManager.getHellephantKills(),

			scoreManager.zombunnyWorth,
			scoreManager.zombearWorth,
			scoreManager.hellephantWorth);

		SceneManager.LoadScene("Next Level");
	}

	public void Pause() {
		Time.timeScale = 0;
		pauseCanvas.SetActive (true);

		// Disabling all sounds
		backgroundMusic.Stop ();
		AudioListener.volume = 0;

		// Setting the options to what the user changed before
		if (!musicOn) {
			musicToggle.isOn = false;
		}
		volumeSlider.value = gameVolume * Constants.SLIDER_VALUE_MAX;

		isPaused = true;
	}

	public void unPause() {
		Time.timeScale = 1;
		pauseCanvas.SetActive (false);

		// Grabbing data from the options menu
		gameVolume = volumeSlider.value / Constants.SLIDER_VALUE_DEFAULT * Constants.VOLUME_VALUE_DEFAULT;
		AudioListener.volume = gameVolume;
		if (musicToggle.isOn) {
			backgroundMusic.Play ();
			musicOn = true;
		} else {
			musicOn = false;
		}

		isPaused = false;
	}

	// Functions called by the Pause canvas
	public void viewHTP() {
		HTPCanvas.SetActive (true);
	}

	public void offHTP() {
		HTPCanvas.SetActive (false);
	}
}