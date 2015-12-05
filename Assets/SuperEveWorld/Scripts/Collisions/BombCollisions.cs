using UnityEngine;
using System.Collections;

public class BombCollisions : MonoBehaviour {

	public GameObject explosion;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "BossZone") {
			return;		
		}
		if (other.gameObject.tag == "Player" && !HUD._invulnerable) {
			if(!HUD._waitingToDie) {
				HUD._playerHealth = 0;
			}
		}
		Object explo = Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy((GameObject)explo, 5);
		Destroy(gameObject);
	}
}
