using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public Vector2 scrollSpeed;
	public Vector2 direction;
	public Vector2 input;
	public float tileSizeZ;

	private Vector3 startPosition;
	private float x;
	private float y;

	void Start ()
	{
		startPosition = transform.position;
	}

	void Update ()
	{
		x = Mathf.Repeat(Time.time * direction.x * scrollSpeed.x + input.x, tileSizeZ);
		y = Mathf.Repeat(Time.time * direction.y * scrollSpeed.y + input.y, tileSizeZ);
		transform.position = new Vector3 (x, y);
		scrollSpeed.x -= (scrollSpeed.x > 0) ? 0.01f : 0f; // Decelerate
		scrollSpeed.y -= (scrollSpeed.y > 0) ? 0.01f : 0f; // Decelerate
	}

	public void Input(Vector2 keyInput) {
		input += keyInput * .1f;
		direction.x = keyInput.x > 0 ? 1 : direction.x;
		direction.x = keyInput.x < 0 ? -1 : direction.x;
		direction.y = keyInput.y > 0 ? 1 : direction.y;
		direction.y = keyInput.y < 0 ? -1 : direction.y;
		scrollSpeed += new Vector2(Mathf.Abs(keyInput.x),Mathf.Abs(keyInput.y)) * 0.1f;
	}
}
