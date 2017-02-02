using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameEntity {

	public Sprite normalSprite;
	public Sprite damagedSprite;
	public SpriteRenderer spriteRendered;
	public GameObject outerSpaceContainer;
	public GameObject rocketPrefab;

	public StateMachine m_stateMachine;
	private bool shooting;
	private float shootFrequency = 0.2f;

	float x;
	float y;
	float z;

	// Use this for initialization
	void Start () {
		shooting = true;
		StartCoroutine (Shoot ());
	}

	public void Revive() {
		spriteRendered.sprite = normalSprite;
		shooting = true;
		StartCoroutine (Shoot ());
	}
	
	// Update is called once per frame
	void Update () {
		Navigation ();
	}

	private IEnumerator Shoot() {
		GameObject rocket = null;
		bool reUsed = false;
		yield return new WaitForSeconds (shootFrequency);

		foreach (Transform child in outerSpaceContainer.transform) {
			if (child.tag == "Mistle" && !child.gameObject.activeSelf) {
				rocket = child.gameObject;
				reUsed = true;
				break;
			}
		}
		if(!reUsed)
			rocket = Instantiate (rocketPrefab, outerSpaceContainer.transform);
		rocket.transform.localPosition = gameObject.transform.localPosition - outerSpaceContainer.transform.localPosition;
		rocket.SetActive (true);
		rocket.GetComponent<Rocket> ().SetAuthor (Rocket.RocketOwner.Player);
		if(shooting)
			StartCoroutine (Shoot ());
	}

	private void Navigation (){
		x = outerSpaceContainer.transform.position.x;
		y = outerSpaceContainer.transform.position.y;
		z = outerSpaceContainer.transform.position.z;

		Debug.Log ("x " + x + " -- y " + y + " -- W " + Screen.width/2 + " -- H " + Screen.height/2);
		if (Input.GetKey (KeyCode.A)) {
			x += 2f;
			x = x > OuterSpaceControl.PLAYERMAXBOUNDS.x? OuterSpaceControl.PLAYERMAXBOUNDS.x: x;
			outerSpaceContainer.transform.position = new Vector3 (x, y, z);
		}
		if (Input.GetKey (KeyCode.W)) {
			y -= 2f;
			y = y < OuterSpaceControl.PLAYERMINBOUNDS.y ? OuterSpaceControl.PLAYERMINBOUNDS.y: y;
			outerSpaceContainer.transform.position = new Vector3 (x, y, z);
		}
		if (Input.GetKey (KeyCode.S)) {
			y += 2f;
			y = y > OuterSpaceControl.PLAYERMAXBOUNDS.y ? OuterSpaceControl.PLAYERMAXBOUNDS.y: y;
			outerSpaceContainer.transform.position = new Vector3 (x, y, z);
		}
		if (Input.GetKey (KeyCode.D)) {
			x -= 2f;
			x = x < OuterSpaceControl.PLAYERMINBOUNDS.x ? OuterSpaceControl.PLAYERMINBOUNDS.x : x;
			outerSpaceContainer.transform.position = new Vector3 (x, y, z);
		}
	}

	public void Die () {
		shooting = false;
		spriteRendered.sprite = damagedSprite;
		GameObject.FindGameObjectWithTag ("ScoreManager").GetComponent<ScoreManager> ().PlayerDefeated ();
		outerSpaceContainer.GetComponent<OuterSpaceControl> ().GameOver ();
	}
}
