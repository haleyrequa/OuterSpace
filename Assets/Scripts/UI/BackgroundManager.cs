using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public Background backgroundPrefab;
	// Use this for initialization
	void Start () {
		for (int x = -10; x <= 10; x += 20) {
			for (int y = 0; y <= 10; y += 10) {
				Instantiate <Background> (backgroundPrefab, Vector3.zero, Quaternion.Euler (new Vector3 (x, y, 0f)))
					.direction = new Vector3(0f, x/10f, 0f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
