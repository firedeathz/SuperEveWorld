using UnityEngine;
using System.Collections;

public class ProjectileCollisions : MonoBehaviour {

	public GameObject enemyExplosion;
	public GameObject boxExplosion;
	public GameObject microExplosion;
	public AudioClip metalSound;
	public int lifeTime = 3;
	private int damage = 2;

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Projectile" ||
		    collider.gameObject.tag == "Trap" ||
		    collider.gameObject.tag == "orb" ||
		    collider.gameObject.tag == "HiddenOrb" ||
		    collider.gameObject.tag == "Teleport" ||
		    collider.gameObject.tag == "Finish" ||
			collider.gameObject.tag == "BossZone" ||
		    collider.gameObject.tag == "InvulnerableShield" || 
		    collider.gameObject.tag == "FireBurning" ||
		    collider.gameObject.tag == "Spikes") {
			return;	
		} else if (collider.gameObject.tag == "Enemy") {
			Object enemyExplo = Instantiate (enemyExplosion, collider.transform.position, Quaternion.identity);
			Destroy (collider.gameObject);
			Destroy ((GameObject)enemyExplo, 4);
			HUD._score += 500;
		} else if (collider.gameObject.tag == "Box") {
			Object boxExplo = Instantiate (boxExplosion, collider.transform.position, Quaternion.identity);
			Destroy (collider.gameObject);
			Destroy ((GameObject)boxExplo, 3);
			HUD._score += 50;
		} else if (collider.gameObject.tag == "Pushable") {
			audio.PlayOneShot(metalSound);
			collider.rigidbody.AddForce(transform.forward * 7, ForceMode.Impulse);
		} else if (collider.gameObject.tag == "Boss") {
			BossBehaviour.bossHealth -= damage / 2;
			GameObject microExplo = (GameObject)Instantiate(microExplosion, transform.position - transform.forward, Quaternion.identity);
			Destroy (microExplo, 3);
		} else if (collider.gameObject.tag == "LeftBossCore") {
			BossBehaviour.leftCoreHP -= damage;
			GameObject microExplo = (GameObject)Instantiate(microExplosion, transform.position - transform.forward, Quaternion.identity);
			Destroy (microExplo, 3);
		} else if (collider.gameObject.tag == "RightBossCore") {
			BossBehaviour.rightCoreHP -= damage;
			GameObject microExplo = (GameObject)Instantiate(microExplosion, transform.position - transform.forward, Quaternion.identity);
			Destroy (microExplo, 3);
		} else if (collider.gameObject.tag == "BossMiniTurret") {
			Object enemyExplo = Instantiate (enemyExplosion, collider.transform.position, Quaternion.identity);
			Destroy (collider.gameObject);
			Destroy ((GameObject)enemyExplo, 4);
			BossBehaviour.turretsAlive--;
		}

		Destroy (gameObject.renderer);
		Destroy (gameObject.collider);
		Destroy (gameObject.rigidbody);	
	}

	void Awake () {
		Destroy (gameObject, lifeTime);
	}
}
