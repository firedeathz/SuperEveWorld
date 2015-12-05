using UnityEngine;
using System.Collections;

public class UpdateProfile : MonoBehaviour {
	
	void Update () {
		GetComponent<TextMesh> ().text = MainMenu.GetPlayerName ();
	}
}
