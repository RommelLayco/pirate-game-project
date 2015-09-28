using UnityEngine;
using System.Collections;

public class topDownShipController : MonoBehaviour {
    public Vector3 targetLocation;
    private int speed;

    // Use this for initialization
    void Start() {
        speed = 1;
        targetLocation = transform.position;
    }

    void Update() {
        bool reachedTarget = atTarget();
        if (!reachedTarget) {
            //Move and RNG of encountering a ship battle
            moveToTarget();

        } else {
            //must be at target
           // Debug.Log("reached Target");
        }
        Debug.Log(targetLocation);
    }
    void moveToTarget() {
        Debug.Log("Moving from " + gameObject.transform.position + " to " + targetLocation);
        //break this down into components.
        Vector3 move = new Vector3(0, 0, 0);
        if (transform.position.x > targetLocation.x) {
            //needs to move left
            move. x = -1;
        } else {
            //needs to move right
            move.x = 1;
        }

        if (transform.position.y > targetLocation.y) {
            //needs to move down
            move.y = -1;
        } else {
            //needs to move up
            move.y = 1;
        }
        transform.position += move * speed * (3 * Time.deltaTime)/2;

    }

    bool atTarget() {
        float dist = Vector3.Distance(transform.position, targetLocation);
        Vector3 distance = transform.position - targetLocation;
        float actualDistance = distance.sqrMagnitude;
        if (actualDistance <= 6) {
            return true;
        } else {
            return false;
        }
    }
    void startShipBattle() {
        Debug.Log("Starting the ship battle sequence");
    }

}
