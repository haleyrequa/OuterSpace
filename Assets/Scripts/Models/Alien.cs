using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : GameEntity {

	public GameObject rocketPrefab;
	public StateMachine m_stateMachine;
	public SteeringBehavoir m_steeringBehavoir;
	public Sprite normalSprite;
	public const float c_siteDepth = 20f;
	public const float c_safeMistleDepth = 0f;
	public const float c_siteAngle = 180f;
	public const float c_alien_flee_speed = 15f;
	public const float c_alien_cruisin_speed = 20f;
	public const float c_alien_chase_speed = 15f;
	private GameObject outerSpaceContainer;
	private float shootFrequency = 0.2f;
	private bool shooting;

	void Awake () {
		outerSpaceContainer = GameObject.FindGameObjectWithTag ("AlienContainer");
		m_steeringBehavoir = new SteeringBehavoir(this, OuterSpaceControl.MINBOUNDS, OuterSpaceControl.MAXBOUNDS, 40f);
		m_stateMachine = new StateMachine(this);
		m_stateMachine.SetCurrentState(AlienTravelState.Instance);
		m_stateMachine.SetGlobalState(AlienGlobalState.Instance);
	}

	void OnEnable() {
		GameObject.FindGameObjectWithTag ("ScoreManager").GetComponent<ScoreManager> ().IncrementSpawnedCount();
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
		rocket.transform.localPosition = gameObject.transform.localPosition;// - outerSpaceContainer.transform.localPosition;
		rocket.SetActive(true);
		rocket.GetComponent<Rocket> ().SetAuthor (Rocket.RocketOwner.Alien);
		if(shooting)
			StartCoroutine (Shoot ());
	}


	public void AttackPlayer()
	{
		if (!shooting) {
			StartCoroutine (Shoot ());
			shooting = true;
		}
	}

	public void StopShooting(){
		shooting = false;
	}

	public void Crash (){
		shooting = false;
		GameObject.FindGameObjectWithTag ("ScoreManager").GetComponent<ScoreManager> ().IncrementKillCount ();
		gameObject.SetActive (false);
	}

	void Update () {
		CheckBounds ();
		m_stateMachine.UpdateState();
		m_steeringBehavoir.UpdateSteering();
	}

	private void CheckBounds() {
		if (transform.position.x > OuterSpaceControl.MAXBOUNDS.x || 
			transform.position.y > OuterSpaceControl.MAXBOUNDS.y || 
			transform.position.x < OuterSpaceControl.MINBOUNDS.x || 
			transform.position.y < OuterSpaceControl.MINBOUNDS.y) {
//			gameObject.SetActive (false);
		}
	}


	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag ("AlienSpaceShip")) {
			Crash ();
		} else if (collision.gameObject.CompareTag ("Player")) {
			collision.gameObject.GetComponent<Player> ().Die ();
			Crash ();
		}
	}

	public StateMachine	  GetFSM()  { return m_stateMachine; }
}
