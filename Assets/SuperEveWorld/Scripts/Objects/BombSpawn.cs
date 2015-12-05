using UnityEngine;
using System.Collections;

public class BombSpawn : MonoBehaviour {

	public GameObject bomb;
	private bool _spawningBomb = false;

	IEnumerator SpawnBomb() {	
		_spawningBomb = true;
		System.Random rand = new System.Random ();
		GameObject bombClone = (GameObject)Instantiate(bomb, transform.position + new Vector3(0,1,0), Quaternion.identity);
		bombClone.rigidbody.AddForce(new Vector3(rand.Next(-3,3), rand.Next(1,5), rand.Next(-3,3)) * 2, ForceMode.Impulse);
		yield return new WaitForSeconds(2);
		_spawningBomb = false;
	}

	void Update () {
		if(!_spawningBomb) {
			StartCoroutine(SpawnBomb());
		}
	}
}
