using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour {

	private GameObject shield;
	public static int bossHealth;
	public static int leftCoreHP;
	public static int rightCoreHP;
	private bool playerIsClose;
	public static bool bossActive;
	private bool _spawnOnCooldown;
	private bool _spawnIceOnCooldown;
	private bool _countingDown;
	private bool _timeOver;
	public static bool bossAlive;
	private int timeLeft;
	public Texture2D healthBar;
	public GameObject bossDeath;
	public GameObject coreExplosion;
	public GameObject turret;

	public GameObject iceFalls;
	public Transform[] spawns;

	public static int turretsAlive = 0;

	private GameObject leftCore;
	private GameObject rightCore;

	public static void ResetStateOnGameOver() {
		leftCoreHP = 50;
		rightCoreHP = 50;
		turretsAlive = 0;
		bossAlive = true;
		bossActive = false;
		bossHealth = 200;
	}

	void KillBoss() {
		bossAlive = false;
		bossActive = false;
		HUD._score += 5000;
		GameObject death = (GameObject)Instantiate (bossDeath, transform.position, Quaternion.identity);
		Destroy (death, 4);
		Destroy (gameObject);
	}

	void DamageBoss(GameObject core) {
		GameObject coreExplo = (GameObject)Instantiate(coreExplosion, core.transform.position, Quaternion.identity);
		Destroy (coreExplo, 4);
		bossHealth -= 75;
		Destroy (core);
	}

	void Start() {
		_spawnOnCooldown = false;
		_spawnIceOnCooldown = false;
		_countingDown = false;
		_timeOver = false;
		bossActive = false;
		playerIsClose = false;
		bossAlive = true;
		timeLeft = 300;
		bossHealth = 200;
		leftCoreHP = 50;
		rightCoreHP = 50;
		shield = GameObject.Find ("Energy Shield");
		leftCore = GameObject.Find ("LeftCore");
		rightCore = GameObject.Find ("RightCore");
	}

	void OnGUI() {
		if(bossActive) {
			int roundedRestSeconds = timeLeft;
			int displaySeconds = roundedRestSeconds % 60;
			int displayMinutes = roundedRestSeconds / 60; 

			GUI.Label (new Rect(Screen.width/2 - 25, 10, 100, 20), "Time Left");
			GUI.Label (new Rect(Screen.width/2 - 15, 25, 100, 20), string.Format ("{0:00}:{1:00}", displayMinutes, displaySeconds));

			GUI.Label (new Rect(Screen.width/4 - 200, Screen.height - 60, 200, 20), "Left Core Health");
			for(int i = 0; i < leftCoreHP; i++) {
				GUI.Label (new Rect(Screen.width/4 - 200 + 2*i, Screen.height - 40, 100, 30), healthBar); 
			}
			GUI.Label (new Rect(Screen.width/4 - 195, Screen.height - 35, 200, 20), leftCoreHP.ToString());

			GUI.Label (new Rect(Screen.width*3/4 + 100, Screen.height - 60, 200, 20), "Right Core Health");
			for(int i = 0; i < rightCoreHP; i++) {
				GUI.Label (new Rect(Screen.width*3/4 + 100 + 2*i, Screen.height - 40, 100, 30), healthBar); 
			}
			GUI.Label (new Rect(Screen.width*3/4 + 105, Screen.height - 35, 200, 20), rightCoreHP.ToString());

			GUI.Label (new Rect(Screen.width/2 - 30, Screen.height - 60, 100, 20), "Boss Health");
			for(int i = 0; i < bossHealth; i++) {
				GUI.Label (new Rect(Screen.width/2 - 200 + 2*i, Screen.height - 40, 100, 30), healthBar); 
			}
			GUI.Label (new Rect(Screen.width/2 - 195, Screen.height - 35, 200, 20), bossHealth.ToString());
		}
	}

	void ActivateShield(bool value) {
		shield.collider.enabled = value;
		shield.renderer.enabled = value;
	}

	IEnumerator SpawnAdds() {
		_spawnOnCooldown = true;
		foreach (Transform spawn in spawns) {
			Instantiate (turret, spawn.position, Quaternion.identity);
			turretsAlive++;
		}
		yield return new WaitForSeconds (45F);
		_spawnOnCooldown = false;
	}

	IEnumerator SpawnIceFalling() {
		_spawnIceOnCooldown = true;
		GameObject iceFall = (GameObject)Instantiate (iceFalls, transform.position + new Vector3(0,10,0), Quaternion.identity);
		Destroy (iceFall, 2);
		yield return new WaitForSeconds (1F);
		_spawnIceOnCooldown = false;
	}

	IEnumerator Countdown() {
		_countingDown = true;
		timeLeft--;
		yield return new WaitForSeconds (1);
		_countingDown = false;
	}

	IEnumerator TimesUp() {
		_timeOver = true;
		GameObject death = (GameObject) Instantiate (bossDeath, HUD._selfPos.position, Quaternion.identity);
		Destroy (death, 4);
		HUD._numlives = 0;
		HUD._playerHealth = 0;
		yield return new WaitForSeconds (0);
	}

	void Update() {
		if (timeLeft < 1 && !_timeOver) {
			StartCoroutine (TimesUp());		
		}

		if(bossActive && !_countingDown) {
			StartCoroutine (Countdown());
		}

		if (turretsAlive > 0 || !playerIsClose) {
			ActivateShield (true);
		}
		else ActivateShield (false);

		if (bossHealth < 151 && bossAlive) {
			if(!_spawnOnCooldown) {
				StartCoroutine(SpawnAdds());		
			}
		}

		if (bossHealth < 51 && bossAlive) {
			if(!_spawnIceOnCooldown) {
				StartCoroutine(SpawnIceFalling());		
			}
		}

		if (bossHealth < 1 && bossAlive) {
			KillBoss();		
		}

		if (leftCoreHP < 1 && leftCore) {
			DamageBoss(leftCore);		
		}
		if (rightCoreHP < 1 && rightCore) {
			DamageBoss(rightCore);
		}

		if (shield) {
			shield.transform.RotateAround(shield.transform.position, new Vector3(0,1,0), Time.deltaTime * 100);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			bossActive = true;
			playerIsClose = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			playerIsClose = false;
		}
	}
}
