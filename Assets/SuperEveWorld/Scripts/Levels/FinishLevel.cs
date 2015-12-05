using UnityEngine;
using System.Collections;

public class FinishLevel : MonoBehaviour {

	public static bool finished = false;

	void OnGUI() {
		if (finished) {
			GUI.Label(new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 200, 120), "LEVEL FINISHED", "box");
			if(GUI.Button(new Rect(Screen.width/2 - 90, Screen.height/2 - 70, 180, 20), "Next Level", "button")) {
				LoadNextLevel();
			}
			if(GUI.Button (new Rect(Screen.width/2 - 90, Screen.height/2 - 40, 180, 20), "Choose Level", "button")) {
				GoToLevelMenu();
			}
			if(GUI.Button(new Rect(Screen.width/2 - 90, Screen.height/2 - 10, 180, 20), "Quit to Menu", "button")) {
				SafeQuitToMenu();
			}
		}
	}

	void SafeQuitToMenu() {
		HideFinishMenu ();
		Application.LoadLevel ("MainMenu");
	}

	void LoadNextLevel() {
		HideFinishMenu ();
		if(Application.loadedLevel < Application.levelCount - 3) {
			Application.LoadLevel (Application.loadedLevel + 1);
		}
		else {
			Application.LoadLevel ("LevelsMenu");
		}
	}

	void GoToLevelMenu() {
		HideFinishMenu ();
		Application.LoadLevel ("LevelsMenu");
	}

	void ShowFinishMenu() {
		finished = true;
		Time.timeScale = 0;
	}

	void HideFinishMenu() {
		finished = false;
		Time.timeScale = 1;
	}

	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "Player") {
			HighScoreManager._instance.SaveHighScore (HUD._playerName, HUD._score);
			ShowFinishMenu();
		}
	}
}
