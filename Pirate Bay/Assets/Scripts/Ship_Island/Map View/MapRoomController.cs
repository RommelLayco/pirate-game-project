using UnityEngine;
using System.Collections;

public class MapRoomController : MonoBehaviour {
//Opens the ExtendableMap scene that controlls the ships location

	void OnMouseDown(){
		clicked ();
	}

	void clicked() {
		Application.LoadLevel("ExtendableMap");
	}
}
