using UnityEngine;
using System.Collections;

// Class holding the number of enemies killed and their worth
// to be passed onto the educational scene (See EducationalManager)
public class DataManager: MonoBehaviour {

	/* Multidimensional array to hold all of the data:
	 * -------------------------------------
	 * | zombunnyKills   | zombunnyWorth   |
	 * | zombearKills    | zombearWorth    |
	 * | hellephantKills | hellephantWorth |
	 * -------------------------------------
	 * */
	int[, ] enemiesSend = new int[3, 2];

	// Accumulating counts throughout all levels
	public static int TOTAL_ZOMBUNNY = 0;
	public static int TOTAL_ZOMBEAR = 0;
	public static int TOTAL_HELLEPHANT = 0;
	public static int TOTAL_SCORE = 0;

	// Called at the end of the level by NextLevel
	public void captureData(int zombunny, int zombear, int hellephant,
		int zombunnyWorth, int zombearWorth, int hellephantWorth) {
		// Capturing the data into an array
		enemiesSend[0, 0] = zombunny;
		enemiesSend[1, 0] = zombear;
		enemiesSend[2, 0] = hellephant;

		enemiesSend[0, 1] = zombunnyWorth;
		enemiesSend[1, 1] = zombearWorth;
		enemiesSend[2, 1] = hellephantWorth;

		// Updating the kill and score counters
		TOTAL_ZOMBUNNY += zombunny;
		TOTAL_ZOMBEAR += zombear;
		TOTAL_HELLEPHANT += hellephant;
		TOTAL_SCORE += zombunny * zombunnyWorth
					+ zombear * zombearWorth
					+ hellephant * hellephantWorth;
	}

	// Accessor method for the EducationalManager
	public int[, ] getEnemies() {
		return enemiesSend;
	}
}