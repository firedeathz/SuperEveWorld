using UnityEngine;
using System.Collections;

public class CrystalCollision : MonoBehaviour {

	public GameObject crystalBreak;
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "BossZone") {
			return;
		}
		if (other.gameObject.tag == "Player" && !HUD._invulnerable) {
			if(!HUD._waitingToDie) {
				HUD._playerHealth -= 10;
			}
		}
		Object crystBreak = Instantiate(crystalBreak, transform.position, Quaternion.identity);
		Destroy((GameObject)crystBreak, 5);
		Destroy(gameObject);
	}
}
