﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IslandController : MonoBehaviour {

	public Vector3 location; // CrewScrollerForIslandSelection position in the world
	public List<IslandController> availableIslands; // Islands that can be reached from this island
	public int level; // Indicates the difficulty of the island
	public string rival; // Indicates which rival controls this island

	private Object IslandLockObject;
	private List<Object> Lines = new List<Object>();

	private GameObject PopUp;
    private GameManager manager;
	// Prefabs for indicators
	public GameObject LockPrefab;
	public GameObject LinePrefab;

    void Awake() {
        manager = GameManager.getInstance();
        PopUp = GameObject.Find ("PopUp");

		HidePopup ();
		// Get the location of the island
		Transform t = gameObject.GetComponent<Transform>();
        location = new Vector3(t.position.x, t.position.y, t.position.z);

		Debug.Log ("Initialised Island");

		// Draw Lock icon if island is not cleared
		if (!manager.GetIslandStatus (location)) {
			DrawLock();
		}

		DrawLines ();
        int rivalRand = Random.Range(1, 4);
        switch (rivalRand)
        {
            case 1:
                Debug.Log("Rival B");
                rival = "B";
                break;
            case 2:
                Debug.Log("Rival W");
                rival = "W";
                break;
            case 3:
                Debug.Log("Rival R");
                rival = "R";
                break;
            default:
                rival = "R";
                break;
        }
		ReDrawLock ();


		// Draw faded lines to show the overall connections between islands
		ReDrawLines ();
    }

	void OnMouseUp() {
		// Check if the island that the ship is currently at has been cleared.
		if (manager.GetCurrentIslandStatus ()) {
			// Allow movement to island if current island is cleared
			setTarget ();
		} else if (!manager.GetCurrentIslandStatus () && manager.GetIslandStatus (gameObject.GetComponent<Transform> ().position)) {
			// Allow movement if target island is cleared
			setTarget ();
		} else if (!manager.GetCurrentIslandStatus () && manager.GetIsland(manager.currentLocation).availableIslands.Contains(this)) {
			Debug.Log("You must clear the current island first");
			ShowPopup("You must explore the current island\nbefore you can move on");
		} else if (manager.GetIsland(manager.currentLocation) != this){
			// Island is not yet available
			Debug.Log("Island at: " + location + "is not available yet");
			ShowPopup("You cannot reach this island from here");
		}
    }

	// Show the specified text in a popup window
	void ShowPopup(string text) {
		Text[] t = PopUp.GetComponentsInChildren<Text> ();
		t [0].text = text;
		PopUp.GetComponent<Canvas> ().enabled = true;
	}

	// Hide the popup window
	public void HidePopup() {
		PopUp.GetComponent<Canvas> ().enabled = false;
	}
	
	// Shade islands to indicate if they are currently reachable
    public void ShowReachable() {
        GameObject[] islands = GameObject.FindGameObjectsWithTag("Island");

        foreach (GameObject g in islands) {
            g.GetComponent<SpriteRenderer>().color = new Color(0.5F, 0.5F, 0.5F);// 434343FF;
        }

		// Ensure islands that are connected to this one are not dimmed
        this.GetComponent<SpriteRenderer>().color = new Color(1F, 1F, 1F);
        foreach (IslandController ic in this.availableIslands) {
            ic.GetComponent<SpriteRenderer>().color = new Color(1F, 1F, 1F);
        }

		ReDrawLines ();
    }

	void DrawLock() {
		// Add lock icon over top of island to show that the player cannot go to it yet
		IslandLockObject = Instantiate(LockPrefab, location, Quaternion.identity);
	}

	// Should be called when lock conditions should be reevaluated
	public void ReDrawLock() {
		if (IslandLockObject != null) {
			Destroy (IslandLockObject);
		}

		// Draw Lock icon if island is not cleared
		IslandController currentIsland = manager.GetIsland (manager.currentLocation);
		if (currentIsland == null) {
			topDownShipController shipCont = GameObject.FindObjectOfType (typeof(topDownShipController)) as topDownShipController;
			currentIsland = manager.GetIsland (shipCont.transform.position);
		}
		if (manager.GetIslandStatus (location)) {
			// Do nothing
		} else if (currentIsland.availableIslands.Contains(this) && !manager.GetCurrentIslandStatus()) {
			DrawLock();
		} else if (!currentIsland.availableIslands.Contains(this) && !manager.GetIslandStatus(location) && this != currentIsland) {
			DrawLock();
		}

	}

	// Draws lines that connect the island to islands that are reachable from this one
	void DrawLines(float alpha=0.2f) {
		Vector3 pos1 = this.transform.position;
		foreach (IslandController island in availableIslands) {

			// Get position of connected island
			Vector3 pos2 = island.transform.position;

			// Instaniate new line
			GameObject go = (GameObject)Instantiate (LinePrefab, location, Quaternion.identity);
			Lines.Add(go);

			// Rotate line so it faces the two islands
			Quaternion rotation = Quaternion.LookRotation (pos1 - pos2, transform.TransformDirection (Vector3.up));
			go.transform.rotation = new Quaternion (0, 0, rotation.z, rotation.w);

			// Move line so it is between the islands
			go.transform.position = Vector3.Lerp (pos1, pos2, 0.5f);

			// Scale line so it reaches between the islands
			Vector3 scale = new Vector3(1,1,1);
			scale.x = Vector3.Distance(pos1, pos2);
			go.transform.localScale = scale;

			// Set the alpha value of line to specified value
			SpriteRenderer sp = go.GetComponent<SpriteRenderer>();
			Color newColor = sp.color;
			newColor.a = alpha;
			sp.color = newColor;
		}
	}

	// Should be called when ship moves to redraw lines
	public void ReDrawLines() {
		foreach(Object line in Lines) {
			Destroy(line);
		}

		Lines.Clear ();
		DrawLines ();
		if (manager.GetIsland (manager.targetLocation) == this) {
			// Draw darker connecting lines for this island as we are currently at it
			DrawLines (1.0f);
		}
	}

	// Sets the target to the island that is clicled on
	private void setTarget() {
		if (manager.GetIsland (manager.currentLocation).availableIslands.Contains (this)) {
			// Setting the persisted targetLocation to be below the new island so that the ship will move towards it.
			manager.targetLocation.x = gameObject.transform.position.x;
			manager.targetLocation.y = gameObject.transform.position.y;

			// Indicate the islands that are now reachable
			ShowReachable ();
		}
	}

}
