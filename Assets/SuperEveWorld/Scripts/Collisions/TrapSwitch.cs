using UnityEngine;
using System.Collections;

public class TrapSwitch : MonoBehaviour {

	private bool beingStepped = false;
	private ArrayList whoIsStepping = new ArrayList ();
	public Material active;
	public Material inactive;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Projectile" ||
		   other.gameObject.tag == "TurretProjectile" ||
		   other.gameObject.tag == "InvulnerableShield") {
			return;
		}
		if(!whoIsStepping.Contains(other)) {
			whoIsStepping.Add(other);

			transform.localScale = new Vector3(2,0.25F,2);
			gameObject.renderer.material = active;
			if(!beingStepped) {
 				OpenMoveableDoor.switchesEnabled++;
				beingStepped = true;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag == "Projectile" ||
		   other.gameObject.tag == "TurretProjectile" ||
		   other.gameObject.tag == "InvulnerableShield") {
			return;
		}
		else if(whoIsStepping.Contains(other)) {
			whoIsStepping.Remove(other);

			if(whoIsStepping.Count < 1) {
				transform.localScale = new Vector3(2,0.5F,2);
				gameObject.renderer.material = inactive;

				if(beingStepped) {
					OpenMoveableDoor.switchesEnabled--;
					beingStepped = false;
				}
			}
		}
	}
}