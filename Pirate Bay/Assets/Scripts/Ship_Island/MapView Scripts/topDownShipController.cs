using UnityEngine;
using System.Collections;

public class topDownShipController : MonoBehaviour {
    private Vector3 targetLocation;
    public int speed = 5;

    // Use this for initialization
    void Start() {
        //TODO get persisted location in here, and set the position of the ship
        //targetLocation = transform.position;


		if (GameObject.Find ("GameManager").GetComponent <GameManager>().currentLocation == new Vector3(-500,-500,-500)) {
			GameObject.Find ("GameManager").GetComponent <GameManager>().currentLocation = transform.position;
		} else {
			transform.position = GameObject.Find ("GameManager").GetComponent <GameManager>().currentLocation;
		}

		if (GameObject.Find ("GameManager").GetComponent <GameManager>().targetLocation == new Vector3(-500,-500,-500)) {
			GameObject.Find ("GameManager").GetComponent <GameManager>().targetLocation = transform.position;
			targetLocation = transform.position;
		} else {
			targetLocation = GameObject.Find ("GameManager").GetComponent <GameManager>().targetLocation;
		}
    }

	void Update(){
		targetLocation = GameObject.Find ("GameManager").GetComponent <GameManager> ().targetLocation;
	}

    void FixedUpdate() {
        bool reachedTarget = atTarget();
        if (!reachedTarget) {
            //Move and RNG of encountering a ship battle
            moveToTarget();
			GameObject.Find ("GameManager").GetComponent <GameManager>().currentLocation = transform.position;

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
