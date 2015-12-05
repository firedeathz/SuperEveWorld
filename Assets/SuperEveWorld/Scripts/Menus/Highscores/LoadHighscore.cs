using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadHighscore : MonoBehaviour {

	public bool isName;
	public int highscorePosition;

	void Update () {
		List<Scores> highscores = HighScoreManager._instance.GetHighScore ();

		if (highscores.Count > highscorePosition) {
			if(isName) {
				GetComponent<TextMesh>().text = highscores[highscorePosition].name;
			}
			else {
				GetComponent<TextMesh>().text = highscores[highscorePosition].score.ToString();
			}
		}
		else {
			GetComponent<TextMesh>().text = "- -";
		}
	}
}