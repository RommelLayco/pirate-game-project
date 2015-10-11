using UnityEngine;
using System.Collections;

public class SailsRoomController : MonoBehaviour {
	
    // Opens the Sails room scene that allows upgrading of the ships cannons
    void OnMouseDown()
    {
        clicked();
    }

    void clicked()
    {
        Application.LoadLevel("SailsRoom");
    }
}
