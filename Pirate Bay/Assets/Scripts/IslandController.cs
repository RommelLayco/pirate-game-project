using UnityEngine;
using System.Collections;

public class IslandController : MonoBehaviour {
    public GameObject ship;

    void Start() {
        ship = GameObject.Find("topDownShip");
        Debug.Log(ship.transform.position);
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
        Debug.Log("On MouseUP");
        Vector2 target = ship.GetComponent<topDownShipController>().targetLocation;
        target.x = gameObject.transform.position.x;
        target.y = gameObject.transform.position.y;
        ship.GetComponent<topDownShipController>().targetLocation = target;
    }

}
