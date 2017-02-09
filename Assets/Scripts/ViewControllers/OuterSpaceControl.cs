using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterSpaceControl : MonoBehaviour {

	public static Vector3 MINBOUNDS = new Vector3 (-170f, -170f, 50f);
	public static Vector3 MAXBOUNDS = new Vector3 (170f, 170f, 50f);
	public static Vector3 PLAYERMINBOUNDS = new Vector3 (-130, -147, 60f);
	public static Vector3 PLAYERMAXBOUNDS = new Vector3 (130, 135, 60f);
	private float spawnBorderDepth = 10f;
	public GameObject alienPrefab;
	private int alienCount = 4;
	private float spawnTime = 3f;
	private bool continueSpawning = true;


	// Use this for initialization
	void Start () {
		//StartCoroutine(SpawnAliens(spawnTime, alienCount));
	}

	private IEnumerator SpawnAliens(float time, int count){
		Spawn (count);
		yield return new WaitForSeconds (time);
		if (continueSpawning) {
			StartCoroutine (SpawnAliens (time, count + 1));
		}
	}

	private void Spawn(int count) {
		int c = 0;
		foreach (Transform child in transform) {
			if (c < count) {
				if (child.tag == "AlienSpaceShip" && !child.gameObject.activeSelf) {
					child.gameObject.SetActive (true);
					child.gameObject.transform.localPosition = GetRandomBorderVector ();
					c++;
				}
			} else {
				return;
			}
		}
		for (int i = c; i < count; i++) {
			GameObject alien = Instantiate (alienPrefab, transform);
			alien.transform.localPosition = GetRandomBorderVector ();
		}
	}

	public void Reset() {
		foreach (Transform child in transform) {
			if(child.tag == "AlienSpaceShip")
				child.GetComponent<Alien> ().Crash ();
			else if(child.tag == "Mistle")
				child.GetComponent<Rocket> ().SelfDestruct ();
		}GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().Revive ();
		continueSpawning = true;
		alienCount = 2;
		StartCoroutine(SpawnAliens(spawnTime, alienCount));
	}

	public void GameOver() {
		continueSpawning = false;
	}

	private Vector3 GetRandomBorderVector()
	{
		switch (Random.Range (0, 3)) {
		case 0: 
			return new Vector3 (Random.Range (MINBOUNDS.x, MAXBOUNDS.x), Random.Range (MAXBOUNDS.y - spawnBorderDepth, MAXBOUNDS.y), 40f);
		case 1: 
			return new Vector3 (Random.Range (MINBOUNDS.x, MINBOUNDS.x + spawnBorderDepth), Random.Range (MINBOUNDS.y, MAXBOUNDS.y), 40f);
		case 2: 
			return new Vector3 (Random.Range (MINBOUNDS.x, MAXBOUNDS.x), Random.Range (MINBOUNDS.y, MINBOUNDS.y + spawnBorderDepth), 40f);
		default: 
			return new Vector3 (Random.Range (MAXBOUNDS.x - spawnBorderDepth, MAXBOUNDS.x), Random.Range (MINBOUNDS.y, MAXBOUNDS.y), 40f);
		}
	}
}
