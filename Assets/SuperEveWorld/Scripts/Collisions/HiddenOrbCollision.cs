using UnityEngine;
using System.Collections;

public class HiddenOrbCollision : MonoBehaviour {

	public GameObject pickUp;
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			Object pickAnim = Instantiate(pickUp, gameObject.transform.position - new Vector3(0F,0.5F,0F), Quaternion.identity);
			HUD._score += 1000;
			HUD._orbs++;
			Destroy((GameObject)pickAnim, 2);
			Destroy(gameObject);
		}
	}
}