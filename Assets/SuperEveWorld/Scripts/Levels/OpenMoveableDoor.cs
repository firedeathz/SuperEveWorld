using UnityEngine;
using System.Collections;

public class OpenMoveableDoor : MonoBehaviour {

	public static int switchesEnabled = 0;
	private Vector3 closedPos;
	public float speed;

	void Start() {
		switchesEnabled = 0;
		closedPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (switchesEnabled == 1) {
			transform.position = Vector3.Lerp(transform.position, closedPos + new Vector3(0,0.5F,0), Time.deltaTime * speed);	
		}
		else if (switchesEnabled == 2) {
			transform.position = Vector3.Lerp(transform.position, closedPos + new Vector3(0,1,0), Time.deltaTime * speed);	
		}
		else if (switchesEnabled == 3) {
			transform.position = Vector3.Lerp(transform.position, closedPos + new Vector3(0,7,0), Time.deltaTime);	
		}
		else {
			transform.position = Vector3.Lerp(transform.position, closedPos, Time.deltaTime * speed / 2);
		}
	}
}
