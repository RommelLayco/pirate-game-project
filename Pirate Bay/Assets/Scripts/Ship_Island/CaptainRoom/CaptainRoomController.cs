﻿using UnityEngine;

public class CaptainRoomController : MonoBehaviour {

	// Opens the Cannons room scene that allows upgrading of the ships cannons
	void OnMouseDown() {
		Application.LoadLevel("CaptainRoom");
	}
}
