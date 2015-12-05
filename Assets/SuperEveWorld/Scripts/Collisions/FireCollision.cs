using UnityEngine;
using System.Collections;

public class FireCollision : MonoBehaviour {

	private bool playerIsBurning = false;
	private bool startedBurning = false;
	public GameObject explosion;

	IEnumerator BurnDamage() {
		startedBurning = true;
		if(!HUD._invulnerable) {
			HUD._playerHealth -= 10;
			if (HUD._playerHealth < 1 && !HUD._waitingToDie) {
				GameObject explo = (GameObject) Instantiate(explosion, HUD._selfPos.position, Quaternion.identity);
				Destroy(explo, 4);
			}
		}
		yield return new WaitForSeconds (0.5F);
		startedBurning = false;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			playerIsBurning = true;
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") {
			if(playerIsBurning && !startedBurning) {
				StartCoroutine(BurnDamage());
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			playerIsBurning = false;		
		}
	}
}
