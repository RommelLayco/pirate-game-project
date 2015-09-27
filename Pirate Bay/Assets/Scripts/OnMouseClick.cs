using UnityEngine;
using System.Collections;

public class OnMouseClick : MonoBehaviour {

	void Update() {
		if (Input.GetMouseButton (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		
			if (hit.collider != null) {
				clicked ();
			}
			//clicked ();
		}
	}

	void clicked(){
		Application.LoadLevel ("Map");
	}
}

	
	