using UnityEngine;
using System.Collections;

public class OnClickBack : MonoBehaviour {

	void Update() {
		if (Input.GetMouseButton (0))
			clicked ();
	}
	
	void clicked(){
		Application.LoadLevel ("Ship");
	}
}
