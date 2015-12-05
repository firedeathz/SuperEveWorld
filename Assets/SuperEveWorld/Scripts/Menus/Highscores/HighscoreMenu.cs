using UnityEngine;
using System.Collections;

public class HighscoreMenu : MonoBehaviour {

	public int distance = 0;
	public bool isBackButton = false;
	public bool isResetButton = false;
	
	void Start() {
		if( !isBackButton || !isResetButton ) {
			renderer.material.color = Color.white;
		}	
	}
	
	void Update () {
		transform.position = new Vector3(transform.position.x + Mathf.Cos(distance*Mathf.PI/180)*0.01F,transform.position.y,transform.position.z);
		distance++;
		distance = distance % 360;
	}
	
	void OnMouseEnter() {
		//change color on mouseover
		if( isBackButton || isResetButton ) {
			renderer.material.color = new Color(0F,0.2F,1F,1F);
		}
	}
	
	void OnMouseExit() {
		//change color on mouse exit
		if( isBackButton || isResetButton ) {
			renderer.material.color = Color.white;
		}
	}
	
	void OnMouseUp() {
		if( isBackButton ) {
			Application.LoadLevel("MainMenu");
		}
		if( isResetButton ) {
			HighScoreManager._instance.ClearLeaderBoard();
		}
	}
}
