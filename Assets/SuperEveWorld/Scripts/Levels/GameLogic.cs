using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public AudioClip victory;
	public AudioClip bossPart1;
	public static bool paused = false;
	private bool victorious = false;
	public Transform bossCheckpoint;

	void OnGUI() {
		if (paused) {
			GUI.Label(new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 200, 120), "GAME PAUSED", "box");
			if(GUI.Button(new Rect(Screen.width/2 - 90, Screen.height/2 - 70, 180, 20), "Resume", "button")) {
				UnPauseGame();
			}
			if(GUI.Button(new Rect(Screen.width/2 - 90, Screen.height/2 - 40, 180, 20), "Restart", "button")) {
				RestartGame();
			}
			if(GUI.Button(new Rect(Screen.width/2 - 90, Screen.height/2 - 10, 180, 20), "Quit to Menu", "button")) {
				SafeQuitToMenu();
			}
		}
	}

	void RestartGame() {
		UnPauseGame ();
		BossBehaviour.ResetStateOnGameOver ();
		Application.LoadLevel (Application.loadedLevel);
	}

	void SafeQuitToMenu() {
		UnPauseGame ();
		Application.LoadLevel ("MainMenu");
	}

	void Start() {
		paused = false;
		victorious = false;
	}

	void PauseGame() {
		Time.timeScale = 0;
		audio.Pause();
		paused = true;
	}

	void UnPauseGame() {
		Time.timeScale = 1;
		audio.Play();
		paused = false;
	}

	void Update() {
		if (Input.GetKeyDown ("escape") && !FinishLevel.finished) {
			if(Time.timeScale < 1) {
				UnPauseGame();
			}
			else {
				PauseGame();
			}
		}

		if(Application.loadedLevelName == "Level2") {
			if (BossBehaviour.bossHealth < 1 && !victorious) {
				if(audio.clip != victory) {
					audio.clip = victory;
					audio.loop = false;
					audio.Play ();
					victorious = true;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "BossZone") {
			if(audio.clip != bossPart1) {
				HUD._checkpoint = bossCheckpoint.position;
				audio.clip = bossPart1;
				audio.loop = true;
				audio.Play ();
			}
		}
	}
}
