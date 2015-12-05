using UnityEngine;
using System.Collections;

public class CrystalSpawn : MonoBehaviour {

	public GameObject crystal;
	public int rangeX;
	public int rangeZ;
	private bool _spawningCrystal = false;

	IEnumerator SpawnCrystal() {
		_spawningCrystal = true;
		System.Random rand = new System.Random ();
		GameObject crystalClone = (GameObject)Instantiate(crystal, transform.position + new Vector3(rand.Next(-rangeX,rangeX),0,rand.Next(-rangeZ,rangeZ)), Quaternion.identity);
		yield return new WaitForSeconds(0.01F);
		_spawningCrystal = false;
	}

	void Update () {
		if(!_spawningCrystal) {
			StartCoroutine(SpawnCrystal());
		}
	}
}
