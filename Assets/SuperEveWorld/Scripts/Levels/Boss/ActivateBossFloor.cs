using UnityEngine;
using System.Collections;

public class ActivateBossFloor : MonoBehaviour {

	void Update() {
		if (!BossBehaviour.bossAlive) {
			transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(34.9F,0.5F,39.9F), Time.deltaTime);
		}
	}
}
