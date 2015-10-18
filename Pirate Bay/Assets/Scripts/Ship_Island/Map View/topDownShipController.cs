using UnityEngine;
using System.Collections;

public class topDownShipController : MonoBehaviour {
    private Vector3 targetLocation;
    private int chanceOfShipBattle;
    public int speed;
    private int shipBattlePossibility = 3500;
    private GameManager manager;
    private bool hasMoved;
	private bool wasRun;
	public GameObject firstIsland;


	// Use this for initialization
    void Awake() {
        manager = GameManager.getInstance();
        chanceOfShipBattle = 0;
        hasMoved = false;
		wasRun = false;

        //Checks that the target position and current position have been initialised, and if not, then they are initialised
        if (manager.currentLocation == new Vector3(-500, -500, -500)) {
            manager.currentLocation = transform.position;
        } else {
            transform.position = manager.currentLocation;
        }

        if (manager.targetLocation == new Vector3(-500, -500, -500)) {
            manager.targetLocation = transform.position;
            targetLocation = transform.position;
        } else {
            targetLocation = manager.targetLocation;
        }
        IslandController targetIsland = GameManager.getInstance().GetIsland(targetLocation);
        targetIsland.ShowReachable();

    }

    void Update() {
        //Getting the updated target location, incase it has been changed, by another island being clicked
        targetLocation = manager.targetLocation;
    }

	void SetClicks(bool enable) {
		GameObject[] islandSprites = GameObject.FindGameObjectsWithTag ("Island");
		foreach (GameObject g in islandSprites) {
			g.GetComponent<Collider2D>().enabled = enable;
		}
	}

    void FixedUpdate() {
        if (!atTarget()){;
            //Move and RNG of encountering a ship battle
            moveToTarget();
            if (shouldShipBattle()) {
                chanceOfShipBattle = 0;
                startShipBattle();
            } else {
                chanceOfShipBattle = chanceOfShipBattle + 1;
                if (chanceOfShipBattle >= shipBattlePossibility) {
                    chanceOfShipBattle = 0;
                }
            }
        } else {
            //must be at target
            chanceOfShipBattle = 0;
        }
        //Updating the stored variable
        manager.currentLocation = transform.position;
    }
    void moveToTarget() {
        //transforms the ship towards its target location.
        Vector3 move = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
        transform.position = move;
        hasMoved = true;
		wasRun = false; // Variable used to track whether lines and locks have been redrawn
		this.SetClicks (false);
    }

    public bool atTarget() {
        // Checks that the ship is close enough to it's target to be considered at the island
        Vector3 distance = transform.position - targetLocation;
        float actualDistance = distance.sqrMagnitude;
        if (actualDistance <= 0.01) {
			this.SetClicks(true);
			hasMoved = false;

			if (!wasRun) {
				// Redraw Lock icons because the ship has moved
				IslandController[] islands = GameObject.FindObjectsOfType (typeof(IslandController)) as IslandController[];
				Debug.Log ("Redrawing stuff");
				foreach (IslandController island in islands) {
					island.ReDrawLock ();
					island.ReDrawLines ();
				}
				
				wasRun = true;
			}

			GameManager.getInstance().islandLevel = GameManager.getInstance().GetIsland(targetLocation).level;
            return true;
        } else {
            return false;
        }
    }


    bool shouldShipBattle() {
        // do RNG in here to check for ship battle
        int random = UnityEngine.Random.Range(1, shipBattlePossibility + 1);
        if (random < chanceOfShipBattle) {
            return true;
        }
        return false;
    }
    void startShipBattle() {
        Application.LoadLevel("ship_battle");
    }
}
