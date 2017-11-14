using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour {

	public Background background;
	public Transform aliens;
	public Transform player;
	public Transform camera;
	private float offset = 0.2f;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			aliens.position = new Vector3 (aliens.position.x + offset, aliens.position.y, aliens.position.z);
			background.Input (new Vector2 (1f, 0f));
		}
		if (Input.GetKey (KeyCode.W)) {
			aliens.transform.position = new Vector3 (aliens.position.x, aliens.position.y - offset, aliens.position.z);
			background.Input (new Vector2 (0f, -1f));
		}
		if (Input.GetKey (KeyCode.S)) {
			aliens.transform.position = new Vector3 (aliens.position.x, aliens.position.y + offset, aliens.position.z);
			background.Input (new Vector2 (0f, 1f));
		}
		if (Input.GetKey (KeyCode.D)) {
			aliens.transform.position = new Vector3 (aliens.position.x - offset, aliens.position.y, aliens.position.z);
			background.Input (new Vector2 (-1f, 0f));
		}
	}
}
