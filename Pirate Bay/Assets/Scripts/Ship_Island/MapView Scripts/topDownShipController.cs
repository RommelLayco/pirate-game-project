using UnityEngine;
using System.Collections;

public class topDownShipController : MonoBehaviour {
    private Vector3 targetLocation;
    //private int chanceOfShipBattle;
    public int speed;

    // Use this for initialization
    void Awake() {
        //chanceOfShipBattle = 0;
        
        
        //TODO get persisted location in here, and set the position of the ship
        if (GameObject.Find("GameManager").GetComponent<GameManager>().currentLocation == new Vector3(-500, -500, -500)) {
            GameObject.Find("GameManager").GetComponent<GameManager>().currentLocation = transform.position;
        } else {
            transform.position = GameObject.Find("GameManager").GetComponent<GameManager>().currentLocation;
        }

        if (GameObject.Find("GameManager").GetComponent<GameManager>().targetLocation == new Vector3(-500, -500, -500)) {
            GameObject.Find("GameManager").GetComponent<GameManager>().targetLocation = transform.position;
            targetLocation = transform.position;
        } else {
            targetLocation = GameObject.Find("GameManager").GetComponent<GameManager>().targetLocation;
        }
    }

    void Update() {
        targetLocation = GameObject.Find("GameManager").GetComponent<GameManager>().targetLocation;
    }

    void FixedUpdate() {
        bool reachedTarget = atTarget();
        if (!reachedTarget) {
            //Move and RNG of encountering a ship battle
            moveToTarget();
            GameObject.Find("GameManager").GetComponent<GameManager>().currentLocation = transform.position;
            if (shouldShipBattle()) {
                startShipBattle();
            }
        } else {
            //must be at target
        }
    }
    void moveToTarget() {
        Vector3 move = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
        transform.position = move;

    }

    bool atTarget() {
        Vector3 distance = transform.position - targetLocation;
        float actualDistance = distance.sqrMagnitude;
        if (actualDistance <= 0.01) {
            return true;
        } else {
            return false;
        }
    }


    bool shouldShipBattle() {
        // do RNG in here to check for ship battle
        return false;
    }
    void startShipBattle() {
        Debug.Log("Starting the ship battle sequence");
    }

}
