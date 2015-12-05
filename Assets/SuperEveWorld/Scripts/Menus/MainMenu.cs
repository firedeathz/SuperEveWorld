using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public bool isQuitButton = false;
	public bool isPlayButton = false;
	public bool isProfileButton = false;
	public bool isHighscoreButton = false;
	private bool _choosingName = false;
	private static string profileName;
	private GameObject wall;
	int distance = 0;

	public static string GetPlayerName() {
		return profileName;
	}

	void OnGUI() {
		if (_choosingName) {
			GUI.Label(new Rect(Screen.width/2 - 130, Screen.height/2 - 50, 200, 100), "Profile Name", "box");
			profileName = GUI.TextField(new Rect(Screen.width/2 - 120, Screen.height/2 - 20, 180, 20), profileName, 10);
			if(Event.current.Equals(Event.KeyboardEvent("None"))) {
				CloseWindow();
			}
			if(GUI.Button(new Rect(Screen.width/2 - 120, Screen.height/2 + 20, 180, 20), "Done")) {
				CloseWindow();
			}
			if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) {
				CloseWindow();
			}
		}
	}

	void CloseWindow() {
		PlayerPrefs.SetString("profile",profileName);
		_choosingName = false;
		wall.collider.enabled = false;
	}

	void Start() {
		profileName = PlayerPrefs.GetString ("profile");
		if (profileName.Length < 1) {
			profileName = "Player1";		
		}
		wall = GameObject.Find("InvisWall");
	}

	void Update() {
		if( isPlayButton ) {
			transform.position += new Vector3(Mathf.Cos(distance*Mathf.PI/180)*0.01F, 0, 0);
		}
		else if( isQuitButton ) {
			transform.position += new Vector3(Mathf.Cos(distance*Mathf.PI/180+Mathf.PI/2)*0.01F, 0, 0);
		}
		else {
			transform.position += new Vector3(Mathf.Cos(distance*Mathf.PI/180+Mathf.PI/4)*0.01F, 0, 0);
		}
		distance++;
		distance = distance % 360;
	}
	
	void OnMouseEnter() {
		//change color on mouseover
		renderer.material.color = new Color(0F,0.2F,1F,1F);
	}
	
	void OnMouseExit() {
		//change color on mouse exit
		renderer.material.color = Color.white;
	}
	
	void OnMouseUp() {
		if( isQuitButton ) {
			//quit game
			Application.Quit();
		}
		else if ( isPlayButton ) {
			//load level
			Application.LoadLevel("LevelsMenu");
		}
		else if ( isProfileButton ) {
			_choosingName = true;
			wall.collider.enabled = true;
		}
		else if ( isHighscoreButton ) {
			//show highscores
			Application.LoadLevel("Highscores");
		}
	}
}
