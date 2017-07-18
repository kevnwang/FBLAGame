using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

// Class to control the objective and level name being displayed
public class ObjectiveManager: MonoBehaviour {

	public int killGoal;     // The objective that can be changed from the Unity application

	Text objectiveText;

	void Awake() {
		objectiveText = GetComponent < Text > ();
	}

	void Start() {
		objectiveText.text = SceneManager.GetActiveScene().name 
							+ ":\nObjective: Kill " + killGoal + " enemies";
	}
}