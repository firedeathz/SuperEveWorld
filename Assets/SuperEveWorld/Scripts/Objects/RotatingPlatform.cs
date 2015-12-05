using UnityEngine;
using System.Collections;

public class RotatingPlatform : MonoBehaviour {

	public int speed;
	public bool clockwise;

	void Update() {
		if (clockwise) {
			transform.RotateAround (transform.position, new Vector3 (0, 0, 1), -Time.deltaTime * speed);	
		}
		else {
			transform.RotateAround (transform.position, new Vector3 (0, 0, 1), Time.deltaTime * speed);
		}
	}
}
