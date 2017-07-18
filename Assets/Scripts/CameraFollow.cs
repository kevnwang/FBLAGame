using UnityEngine;
using System.Collections;

// Class to control the camera, to make it follow the character as it moves
public class CameraFollow: MonoBehaviour {

	public Transform playerTransform;     // Reference to the player's position
	public float smoothing = 5.0f;

	Vector3 offset;

	// Start is called before the first frame
	// Used to set initial game states
	void Start() {
		offset = transform.position - playerTransform.position;
	}

	// FixedUpdate is called every fixed frame
	// In this case, when dealing with the player's rigidbody
	void FixedUpdate() {
		Vector3 targetCamPos = playerTransform.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}