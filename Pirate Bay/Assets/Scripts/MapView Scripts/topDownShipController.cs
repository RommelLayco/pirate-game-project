using UnityEngine;
using System.Collections;

public class topDownShipController : MonoBehaviour {
    public Vector3 targetLocation;
    public int speed = 5;
    private int travelDist = 1;

    // Use this for initialization
    void Start() {
        
        targetLocation = transform.position;
    }

    void FixedUpdate() {
        bool reachedTarget = atTarget();
        if (!reachedTarget) {
            //Move and RNG of encountering a ship battle
            moveToTarget();

        } else {
            //must be at target
            Debug.Log("reached Target");
        }
    }
    void moveToTarget() {
        Vector3 move = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
        transform.position = move;

    }

    bool atTarget() {
        float dist = Vector3.Distance(transform.position, targetLocation);
        Vector3 distance = transform.position - targetLocation;
        float actualDistance = distance.sqrMagnitude;
        if (actualDistance <= 0.01) {
            return true;
        } else {
            return false;
        }
    }
    void startShipBattle() {
        Debug.Log("Starting the ship battle sequence");
    }

}
