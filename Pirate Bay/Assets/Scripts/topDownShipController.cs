using UnityEngine;
using System.Collections;

public class topDownShipController : MonoBehaviour {
    public Vector3 targetLocation;
    private int speed;
    private int travelDist = 1;

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
            move. x = -travelDist;
        } else {
            //needs to move right
            move.x = travelDist;
        }

        if (transform.position.y > targetLocation.y) {
            //needs to move down
            move.y = -travelDist;
        } else {
            //needs to move up
            move.y = travelDist;
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
