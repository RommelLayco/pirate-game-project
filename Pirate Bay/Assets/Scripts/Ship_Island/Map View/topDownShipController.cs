using UnityEngine;
using System.Collections;

public class topDownShipController : MonoBehaviour {
    private Vector3 targetLocation;
    private int chanceOfShipBattle;
    public int speed;
    private int shipBattlePossibility = 10000;

    // Use this for initialization
    void Awake() {
        chanceOfShipBattle = 0;

        //Checks that the target position and current position have been initialised, and if not, then they are initialised
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
        //Getting the updated target location, incase it has been changed, by another island being clicked
        targetLocation = GameObject.Find("GameManager").GetComponent<GameManager>().targetLocation;
    }

    void FixedUpdate() {
        if (!atTarget()){;
            //Move and RNG of encountering a ship battle
            moveToTarget();
            if (shouldShipBattle()) {
                startShipBattle();
            } else {
                chanceOfShipBattle = (chanceOfShipBattle++) % shipBattlePossibility;
            }
        } else {
            //must be at target
        }
        //Updating the stored variable
        GameObject.Find("GameManager").GetComponent<GameManager>().currentLocation = transform.position;
    }
    void moveToTarget() {
        //transforms the ship towards its target location.
        Vector3 move = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
        transform.position = move;
    }

    bool atTarget() {
        //Checks that the ship is close enough to it's target to be considered at the island
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
        if (UnityEngine.Random.Range(1, shipBattlePossibility + 1) < chanceOfShipBattle) {
            return true;
        }
        return false;
    }
    void startShipBattle() {
        Debug.Log("Starting the ship battle sequence");
    }

}
