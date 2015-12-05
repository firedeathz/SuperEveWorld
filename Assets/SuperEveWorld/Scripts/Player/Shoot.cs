using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject projectilePrefab;
	private GameObject spawn;

	void Start() {
		spawn = GameObject.Find ("ProjectileSpawn");
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown (0) && !HUD._waitingToDie && !GameLogic.paused && !FinishLevel.finished) 
		{
			GameObject projectile = (GameObject)Instantiate(projectilePrefab, spawn.transform.position, transform.rotation);	
			
			projectile.rigidbody.AddForce(transform.forward * 50, ForceMode.Impulse);
		}
	}
}
