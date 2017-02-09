using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public Vector2 scrollSpeed;
	public Vector2 direction;
	public Vector2 input;
	public float tileSizeZ;
	public float zdepth;
	public float rotation;

	private Vector3 startPosition;
	private float x;
	private float y;
	private float z;

	void Start ()
	{
		startPosition = transform.position;
		tileSizeZ = tileSizeZ * Mathf.Cos (Mathf.Deg2Rad * rotation);
		zdepth = tileSizeZ * Mathf.Sin (Mathf.Deg2Rad * rotation);
	}

	void Update ()
	{
		x = Mathf.Repeat(Time.time * direction.x * scrollSpeed.x + input.x, tileSizeZ);
		y = Mathf.Repeat(Time.time * direction.y * scrollSpeed.y + input.y, tileSizeZ);
		z = Mathf.Lerp (-zdepth/2, zdepth/2, y / tileSizeZ);
		transform.position = new Vector3 (x, y, z);
		//scrollSpeed.x -= (scrollSpeed.x > 0) ? 0.01f : 0f; // Decelerate
		//scrollSpeed.y -= (scrollSpeed.y > 0) ? 0.01f : 0f; // Decelerate
	}

	public void Input(Vector2 keyInput) {
		input += keyInput * .1f;
		direction.x = keyInput.x > 0 ? 1 : direction.x;
		direction.x = keyInput.x < 0 ? -1 : direction.x;
		direction.y = keyInput.y > 0 ? 1 : direction.y;
		direction.y = keyInput.y < 0 ? -1 : direction.y;
//		scrollSpeed += new Vector2(Mathf.Abs(keyInput.x),Mathf.Abs(keyInput.y)) * 0.1f;
	}
}
