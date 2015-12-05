using UnityEngine;
using System.Collections;

public class ExplosiveTrap : MonoBehaviour {

	bool playerIsOn;
	public GameObject explosion;
	private GameObject steppedOn;
	
	void Update() {
		if(playerIsOn) {
			gameObject.transform.localScale = new Vector3(2F, 0.25F, 2F);
		}
		else {
			gameObject.transform.localScale = new Vector3(2F, 0.5F, 2F);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if(!playerIsOn) {
			if(other.gameObject.tag == "Player" && !HUD._invulnerable) {
				playerIsOn = true;
				var explode = Instantiate(explosion, transform.position, Quaternion.identity);
				Destroy(explode, 5);
				if(!HUD._waitingToDie) {
					StartCoroutine(HUD.Die());
				}
			}
			if(other.gameObject.tag == "Pushable" || other.gameObject.tag == "Box") {
				playerIsOn = true;
				var explode = Instantiate(explosion, transform.position, Quaternion.identity);
				Destroy(explode, 5);		
			}
			steppedOn = other.gameObject;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(steppedOn == other.gameObject) {
			if(other.gameObject.tag == "Player" ||
			   other.gameObject.tag == "Pushable" ||
			   other.gameObject.tag == "Box") {
				playerIsOn = false;
			}
		}
	}
}
