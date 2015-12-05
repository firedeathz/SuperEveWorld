using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	public static Transform _selfPos;
	public static int _numlives;
	public static int _playerHealth;
	public Texture2D _tex;
	public static int _score;
	public static string _playerName;
	public static bool _dead;
	public static int _orbs;
	public Texture2D _orbTex;
	public Texture2D _healthBar;
	public Texture2D _barYellow;
	public Texture2D _barRed;
	public static Vector3 _checkpoint;
	int _totalOrbs;
	public static bool _waitingToDie;
	public static bool _invulnerable;
	private GameObject shield;


	// Use this for initialization
	void Start () {
		shield = GameObject.Find ("InvulnerableShield");
		StartCoroutine (Invulnerability());
		_dead = false;
		_waitingToDie = false;
		_selfPos = transform;
		_numlives = 5;
		_playerHealth = 100;
		_score = 0;
		_orbs = 0;
		_playerName = PlayerPrefs.GetString("profile");
		if (Application.loadedLevelName == "Level1") {
			_checkpoint = new Vector3 (0F, 1.5F, -20F);
		}
		else if (Application.loadedLevelName == "Level2") {
			_checkpoint = new Vector3 (-80F, 28.75F, -214);
		}
		else if (Application.loadedLevelName == "Level3") {
			_checkpoint = new Vector3 (0F, 1.5F, -30F);	
		}
		_totalOrbs = GameObject.FindGameObjectsWithTag ("HiddenOrb").Length;
	}

	IEnumerator Invulnerability() {
		_invulnerable = true;
		shield.renderer.enabled = true;
		shield.collider.enabled = true;
		yield return new WaitForSeconds (2);
		shield.renderer.enabled = false;
		shield.collider.enabled = false;
		_invulnerable = false;
	}

	public static IEnumerator Die() {
		_waitingToDie = true;
		yield return new WaitForSeconds (1.5F);
		_dead = true;
	}

	void SafeGameOver() {
		BossBehaviour.ResetStateOnGameOver ();
		HighScoreManager._instance.SaveHighScore (_playerName, _score);
		_numlives = 5;
		_score = 0;
		_dead = false;
		_orbs = 0;
		_playerHealth = 100;
		_waitingToDie = false;
		StartCoroutine (Invulnerability ());
		Application.LoadLevel ("Highscores");
	}

	void CheckLives() {
		if (_numlives < 1) {
			SafeGameOver();
		} else {
			Respawn();
		}
	}

	void Respawn() {
		_playerHealth = 100;
		_dead = false;
		_waitingToDie = false;
		StartCoroutine (Invulnerability ());
		gameObject.transform.localScale = new Vector3(2,2,2);
		gameObject.rigidbody.isKinematic = false;
		gameObject.transform.position = _checkpoint;
		gameObject.transform.rotation = Quaternion.identity;
	}
	
	public static void SaveCheckPoint(Vector3 checkpoint) {
		_checkpoint = checkpoint;
	}

	void Update() {
		if (_playerHealth < 1 && !_waitingToDie) {
			StartCoroutine(Die());		
		}

		if (shield) {
			shield.transform.RotateAround(shield.transform.position, new Vector3(0,1,0), Time.deltaTime * 150);
		}
	}

	void LateUpdate() {
		if (_dead) {
			_numlives--;
			CheckLives();
		}
	}



	void OnGUI() {
		for (int i = 0; i < _numlives; i++) {
			GUI.Label (new Rect (10 + 30*i, 10, 30, 30), _tex);	
		}
		GUI.Label (new Rect (10, 45, 60, 30), "Health: ");
		for (int i = 0; i < _playerHealth; i++) {
			if(_playerHealth < 26) {
				GUI.Label (new Rect (60 + i, 40, 200, 30), _barRed);
			}
			else if(_playerHealth < 51) {
				GUI.Label (new Rect (60 + i, 40, 200, 30), _barYellow);
			}
			else GUI.Label (new Rect (60 + i, 40, 200, 30), _healthBar);
		}
		GUI.Label (new Rect (65, 45, 60, 30), _playerHealth.ToString());

		GUI.Label (new Rect (10, 70, 50, 50), "Score: ");
		GUI.Label (new Rect (55, 70, 100, 100), _score.ToString());
		GUI.Label (new Rect (10, 90, 200, 70), "Hidden Orbs Collected (" + _orbs + " / " + _totalOrbs + "): ");

		for (int i = 0; i < _orbs; i++) {
			GUI.Label (new Rect (10 + 50*i, 110, 50, 90), _orbTex);	
		}
	}
}
