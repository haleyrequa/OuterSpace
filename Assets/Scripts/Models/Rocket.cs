using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : GameEntity {

	public enum RocketOwner
	{
		Player, Alien
	};
	public Sprite sprite;
	public Sprite damagedSprite;
	public SpriteRenderer spriteRenderer;
	private bool enableFullPower;
	private RocketOwner owner;

	// Use this for initialization
	void OnEnable () {
		enableFullPower = false;
		gameObject.GetComponent<Rigidbody> ().velocity = gameObject.transform.up * 50;

	}
	public void SetAuthor(RocketOwner rocketOwner) {
		owner = rocketOwner;
		enableFullPower = true;
		spriteRenderer.color = rocketOwner == RocketOwner.Player ? Color.red : Color.yellow;
	}
	void OnCollisionEnter(Collision collision) {
		if (enableFullPower) {
			if (collision.gameObject.CompareTag ("AlienSpaceShip") && owner == RocketOwner.Player) {
				collision.gameObject.GetComponent<Alien> ().Crash ();
				SelfDestruct ();
			} else if (collision.gameObject.CompareTag ("Player") && owner == RocketOwner.Alien) {
				collision.gameObject.GetComponent<Player> ().Die ();
				SelfDestruct ();
			}
		}
	}

	public void SelfDestruct () {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		CheckBounds ();
	}

	private void CheckBounds() {
		if (transform.localPosition.x > OuterSpaceControl.MAXBOUNDS.x || 
			transform.localPosition.y > OuterSpaceControl.MAXBOUNDS.y || 
			transform.localPosition.x < OuterSpaceControl.MINBOUNDS.x || 
			transform.localPosition.y < OuterSpaceControl.MINBOUNDS.y) {
			SelfDestruct ();
		}
	}
}
