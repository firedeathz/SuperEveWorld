using UnityEngine;
using System.Collections;

public class TurretProjectileCollisions : MonoBehaviour {

	public int lifeTime = 3;
	public GameObject playerExplosion;
	public AudioClip metalSound;
	
	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Projectile" ||
		    collider.gameObject.tag == "Trap" ||
		    collider.gameObject.tag == "orb" ||
		    collider.gameObject.tag == "HiddenOrb" ||
		    collider.gameObject.tag == "Teleport" ||
		    collider.gameObject.tag == "Finish" ||
		    collider.gameObject.tag == "BossZone" || 
		    collider.gameObject.tag == "EnergyShield" || 
		    collider.gameObject.tag == "FireBurning" ||
		    collider.gameObject.tag == "Spikes") {
			return;	
		}
		else if(collider.gameObject.tag == "Player") {
			if(!HUD._waitingToDie) {
				HUD._playerHealth -= 25;
			}
			Object playerExplo = Instantiate (playerExplosion, collider.transform.position, Quaternion.identity);
			Destroy ((GameObject)playerExplo, 4);
		} 
		else if (collider.gameObject.tag == "Pushable") {
			audio.PlayOneShot(metalSound);
			collider.rigidbody.AddForce(gameObject.transform.forward * 2, ForceMode.Force);
		}
		
		Destroy (gameObject.renderer);
		Destroy (gameObject.collider);
		Destroy (gameObject.rigidbody);	
	}
	
	void Awake () {
		Destroy (gameObject, lifeTime);
	}
}