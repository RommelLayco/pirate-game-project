using UnityEngine;
using System.Collections;

public class IslandController : MonoBehaviour {
    private GameObject ship;

    void Start() {
        ship = GameObject.Find("topDownShip");
    }

    void Update() {
       /* foreach (Touch t in Input.touches) {
            if (t.phase == TouchPhase.Ended) {
                bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);
                if (contained) {
                    Debug.Log("LEEDLE LEEDLE LEEDLE LEE");
                }
            }
        }
        */
    }
    void OnMouseUp() {
        Vector2 target = ship.GetComponent<topDownShipController>().targetLocation;
        target.x = gameObject.transform.position.x;
        target.y = gameObject.transform.position.y;
        ship.GetComponent<topDownShipController>().targetLocation = target;
    }

}
