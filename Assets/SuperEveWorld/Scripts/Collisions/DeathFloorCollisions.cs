using UnityEngine;
using System.Collections;

public class DeathFloorCollisions : MonoBehaviour {

	public GameObject explosion;

	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "Player") {
			if(!HUD._waitingToDie) {
				HUD._playerHealth = 0;
			}
			Object touchAnim = Instantiate(explosion, other.transform.position, Quaternion.identity);
			Destroy((GameObject)touchAnim, 3);
		}
		else if(other.gameObject.tag == "Box" ||
		        other.gameObject.tag == "Pushable" ||
		        other.gameObject.tag == "Enemy") {
			Object touchAnim = Instantiate(explosion, other.transform.position, Quaternion.identity);
			Destroy((GameObject)touchAnim, 3);
			Destroy(other.gameObject);
		}
	}
}
