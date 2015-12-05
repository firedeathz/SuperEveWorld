using UnityEngine;
using System.Collections;

public class SpikeCollision : MonoBehaviour {
	
	public GameObject explosion;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player" && !HUD._invulnerable) {
			Object touchAnim = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
			if(!HUD._waitingToDie) {
				HUD._playerHealth = 0;
			}
			Destroy((GameObject)touchAnim, 3);
		}
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "Player" && !HUD._invulnerable) {
			Object touchAnim = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
			if(!HUD._waitingToDie) {
				HUD._playerHealth = 0;
			}
			Destroy((GameObject)touchAnim, 3);
		}
	}
	
}
