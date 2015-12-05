using UnityEngine;
using System.Collections;

public class CloseBossDoor : MonoBehaviour {

	private Vector3 startPos;

	void Start() {
		startPos = transform.position;
	}

	void Update() {
		if (BossBehaviour.bossActive) {
			transform.position = Vector3.Lerp(transform.position,
			                                  new Vector3(transform.position.x,
			            					              3.25F,
			            								  transform.position.z),
			                                  Time.deltaTime*0.5F);		
		}
		else {
			transform.position = Vector3.Lerp(transform.position,
			                                  new Vector3(transform.position.x,
			            								  -9.3F,
			           									  transform.position.z),
			                                  Time.deltaTime*0.5F);
		}
	}
}
