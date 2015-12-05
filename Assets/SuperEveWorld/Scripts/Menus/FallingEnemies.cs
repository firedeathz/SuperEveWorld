using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Rendering;

public class FallingEnemies : MonoBehaviour {

	public GameObject enemyPrefab;
	private bool _enemySpawning = false;

	IEnumerator SpawnEnemy() {
		_enemySpawning = true;
		System.Random rand = new System.Random ();

		GameObject enemy = (GameObject) Instantiate(enemyPrefab, new Vector3(40 + rand.Next(0,10), 40, 40 + rand.Next(0, 10)), new Quaternion());
		int scale = rand.Next(1,3);
		enemy.transform.localScale = new Vector3(scale,scale,scale);
		Destroy(enemy, 15);

		yield return new WaitForSeconds (2);
		_enemySpawning = false;
	}

	// Update is called once per frame
	void Update () {
		if(!_enemySpawning) {
			StartCoroutine (SpawnEnemy ());
		}
	}
}
