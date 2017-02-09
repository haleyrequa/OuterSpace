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

	// Use this for initialization
	void Start () {
	//	shooting = true;
	//	StartCoroutine (Shoot ());
	}

	public void Revive() {
		spriteRendered.sprite = normalSprite;
		shooting = true;
		StartCoroutine (Shoot ());
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

	public void Die () {
		shooting = false;
		spriteRendered.sprite = damagedSprite;
		GameObject.FindGameObjectWithTag ("ScoreManager").GetComponent<ScoreManager> ().PlayerDefeated ();
		outerSpaceContainer.GetComponent<OuterSpaceControl> ().GameOver ();
	}
}
