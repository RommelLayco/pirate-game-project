using UnityEngine;
using System.Collections;

public class IslandController : MonoBehaviour {
    private GameObject ship;

    void Start() {
        ship = GameObject.Find("topDownShip");
    }

    void OnMouseUp() {
        Vector2 target = ship.GetComponent<topDownShipController>().targetLocation;
        target.x = gameObject.transform.position.x;
        target.y = gameObject.transform.position.y - 1;
        ship.GetComponent<topDownShipController>().targetLocation = target;
    }

}
