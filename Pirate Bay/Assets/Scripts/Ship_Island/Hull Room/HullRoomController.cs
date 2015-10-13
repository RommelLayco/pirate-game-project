using UnityEngine;
using System.Collections;

public class HullRoomController : MonoBehaviour {

    // Opens the Sails room scene that allows upgrading of the ships cannons
    void OnMouseDown()
    {
        Application.LoadLevel("HullRoom");
    }
}
