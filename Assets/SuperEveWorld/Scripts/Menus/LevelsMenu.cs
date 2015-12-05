using UnityEngine;
using System.Collections;

public class LevelsMenu : MonoBehaviour {

	public bool isBackButton = false;
	public bool isLvl1Button = false;
	public bool isLvl2Button = false;
	public bool isLvl3Button = false;
	public bool isWord = false;
	private int distance = 0;
	
	void Update() {
		if( isLvl1Button || isLvl2Button || isLvl3Button ) {
			transform.position += new Vector3(0,Mathf.Cos(distance*Mathf.PI/180+Mathf.PI/4) * 0.01F,0);
		}
		else if( isWord ) {
			transform.position += new Vector3(0,Mathf.Cos(distance*Mathf.PI/180+Mathf.PI/2) * 0.01F,0);
		}
		distance++;
		distance = distance % 360;
	}
	
	void OnMouseEnter() {
		if( isLvl1Button || isLvl2Button || isLvl3Button) {
			transform.position += new Vector3(0.5F,0,0.5F);
		}
		renderer.material.color = new Color(0,0.2F,1,1);
	}
	
	void OnMouseExit() {
		if( isLvl1Button || isLvl2Button || isLvl3Button) {
			transform.position -= new Vector3(0.5F,0,0.5F);
			renderer.material.color = new Color(1F,169.0F/255.0F,0F,1F);
		}
		else if( isWord ) {
			renderer.material.color = Color.white;
		}
	}
	
	void OnMouseUp() {
		if( isBackButton ) {
			//quit game
			Application.LoadLevel("MainMenu");
		}
		else if ( isLvl1Button ) {
			// Load level 1
			Application.LoadLevel("Level1");
		}
		else if ( isLvl2Button ) {
			// Load level 2
			Application.LoadLevel("Level2");
		}
		else if ( isLvl3Button ) {
			Application.LoadLevel("Level3");
		}
	}
}
