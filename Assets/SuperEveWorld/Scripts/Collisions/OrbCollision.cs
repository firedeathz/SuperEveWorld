using UnityEngine;
using System.Collections;

public class OrbCollision : MonoBehaviour {

	public GameObject pickUp;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			Object pickAnim = Instantiate(pickUp, other.transform.position, Quaternion.identity);
			HUD._score += 100;
			Destroy((GameObject)pickAnim, 3);
			Destroy(gameObject);
		}
	}

}
