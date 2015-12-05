using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public Transform teleportLocation;
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			other.transform.position = teleportLocation.position;
			HUD.SaveCheckPoint(transform.position);
		}
	}
}
