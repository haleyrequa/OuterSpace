using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text countDownText;
	public Text gameOverText;
	public OuterSpaceControl outerSpaceController;
	private static int aliensSpawned = 0;
	private static int aliensKilled = 0; 
	private static bool playerKilled = false;
	private string[] inspirationalQuote = new string[]{
		" - YOUR AN EXPERT",
		" - BULLSEYE",
		" - BEST SHOT IN THE GALAXY",
		" - YAAAAAAAAAASSSSSS",
		" - TOO COOL FOR SCHOOL",
	};

	void Awake() {
		countDownText.CrossFadeAlpha (0f, 0f, true);
		gameOverText.CrossFadeAlpha (0f, 0f, true);
	}

	public void IncrementSpawnedCount() {
		aliensSpawned++;
		UpdateGUI ();
	}

	public void IncrementKillCount(){
		aliensKilled++;
		UpdateGUI ();
	}

	public void PlayerDefeated(){
		playerKilled = true;
		UpdateGUI ();
		StartCoroutine (CountDownToReset ());
	}

	private IEnumerator CountDownToReset() {
		countDownText.CrossFadeAlpha (1f, 0f, true);
		gameOverText.CrossFadeAlpha (1f, 0f, true);
		for (int i = 5; i > 0; i--) {
			countDownText.text = i.ToString ();
			yield return new WaitForSeconds (1f);
		}
		countDownText.text = "GO!";
		countDownText.CrossFadeAlpha (0f, 0.5f, true);
		gameOverText.CrossFadeAlpha (0f, 0.5f, true);
		PlayAgain ();
	}

	private void PlayAgain () {
		outerSpaceController.Reset ();
		playerKilled = false;
		aliensKilled = 0;
		aliensSpawned = 0;
		UpdateGUI ();
	}

	public void UpdateGUI (){
		scoreText.text = playerKilled ? "" : "Aliens Perished : " + aliensKilled + " --- Aliens Spawned : " + aliensSpawned;
		//scoreText.text += !playerKilled & aliensKilled % 5 == 0 && aliensKilled != 0 ? inspirationalQuote [Random.Range(0, inspirationalQuote.Length - 1)] : "";
	}

}
