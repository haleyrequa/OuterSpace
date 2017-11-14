using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	public Vector3 scrollSpeed;
	public Vector3 direction;
	public Vector3 acceleration; 
	public float tileSizeZ;
	public float zdepth;

	private Vector3 startPosition;
	private float x;
	private float y;
	private float z;

	void Start ()
	{
		startPosition = transform.position;
		tileSizeZ = tileSizeZ * Mathf.Cos (Mathf.Deg2Rad * transform.rotation.eulerAngles.x);
		zdepth = tileSizeZ * Mathf.Sin (Mathf.Deg2Rad * transform.rotation.eulerAngles.x) / 2f;
	}

	void Update ()
	{
		x = Mathf.Repeat(Time.time * direction.x * scrollSpeed.x + acceleration.x, tileSizeZ);
		y = Mathf.Repeat(Time.time * direction.y * scrollSpeed.y + acceleration.y, tileSizeZ);
		z = Mathf.Lerp (0f, zdepth*2f, y / tileSizeZ);
		transform.position = startPosition + new Vector3 (x, y, z);
		//scrollSpeed.x -= (scrollSpeed.x > 0) ? 0.01f : 0f; // Decelerate
		//scrollSpeed.y -= (scrollSpeed.y > 0) ? 0.01f : 0f; // Decelerate
	}

	public void Input(Vector3 keyInput) {
		acceleration += keyInput * .1f;
		direction.x = keyInput.x > 0 ? 1 : keyInput.x < 0 ? -1 : direction.x;
		direction.y = keyInput.y > 0 ? 1 : keyInput.y < 0 ? -1 : direction.y;
//		scrollSpeed += new Vector3(Mathf.Abs(keyInput.x),Mathf.Abs(keyInput.y)) * 0.1f;
	}
}
