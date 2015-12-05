using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	public Transform pointB;
	private Vector3 pointA;
	public float speed = 0.5F;

	void Start () {
		pointA = transform.position;
	}

	void Update () {
		float i = Mathf.PingPong(Time.time * speed, 1);
		transform.position = Vector3.Lerp(pointA, pointB.position, i);
	}
}
