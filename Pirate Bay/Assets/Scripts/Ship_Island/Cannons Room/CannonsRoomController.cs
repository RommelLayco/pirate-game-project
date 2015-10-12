using UnityEngine;

public class CannonsRoomController : MonoBehaviour {

	// Opens the Cannons room scene that allows upgrading of the ships cannons
	void OnMouseDown() {
		Application.LoadLevel("CannonsRoom");
	}
}
