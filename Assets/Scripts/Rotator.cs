using UnityEngine;
using System.Collections;

// Tied to each pickup, causes the pickup to rotate
public class Rotator : MonoBehaviour {

	void Update () {
		transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
	}
}
