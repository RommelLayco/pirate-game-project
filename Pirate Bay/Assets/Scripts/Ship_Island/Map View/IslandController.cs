using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IslandController : MonoBehaviour {

	public Vector3 location; // CrewScrollerForIslandSelection position in the world
	public List<IslandController> availableIslands; // Islands that can be reached from this island
	public int level; // Indicates the difficulty of the island
	public string rival; // Indicates which rival controls this island

	// Prefabs for indicators
	public GameObject LockPrefab;
	public GameObject LinePrefab;

    void Awake() {

		GameManager m = GameManager.getInstance ();

		// Get the location of the island
		Transform t = gameObject.GetComponent<Transform>();
        location = new Vector3(t.position.x, t.position.y, t.position.z);

		Debug.Log ("Initialised Island");

		// Draw Lock icon if island is not cleared
		if (!m.GetIslandStatus (location)) {
			DrawLock();
		}

		// Draw faded lines to show the overall connections between islands
		DrawLines ();

    }

	void OnMouseUp() {
		GameManager m = GameManager.getInstance ();
		// Check if the island that the ship is currently at has been cleared.
		if (m.GetCurrentIslandStatus ()) {
			// Allow movement to island if current island is cleared
			setTarget();
		} else if(!m.GetCurrentIslandStatus () && m.GetIslandStatus(gameObject.GetComponent<Transform>().position)){
			// Allow movement if target island is cleared
			setTarget();
		} else {
			// Island is not yet available
			Debug.Log("Island at: " + location + "is not available yet");
		}
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

		GameManager m = GameManager.getInstance ();
		if (m.GetIsland (m.targetLocation) == this) {
			// Draw darker connecting lines for this island as we are currently at it
			DrawLines (1.0f);
		}
    }

	void DrawLock() {
		// Add lock icon over top of island to show that the player cannot go to it yet
		Instantiate(LockPrefab, location, Quaternion.identity);
	}

	// Draws lines that connect the island to islands that are reachable from this one
	void DrawLines(float alpha=0.2f) {
		Vector3 pos1 = this.transform.position;
		foreach (IslandController island in availableIslands) {

			// Get position of connected island
			Vector3 pos2 = island.transform.position;

			// Instaniate new line
			GameObject go = (GameObject)Instantiate (LinePrefab, location, Quaternion.identity);

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

	// Sets the target to the island that is clicled on
	private void setTarget() {
		if (GameManager.getInstance ().GetIsland (GameManager.getInstance ().currentLocation).availableIslands.Contains (this)) {
			// Setting the persisted targetLocation to be below the new island so that the ship will move towards it.
			GameManager.getInstance ().targetLocation.x = gameObject.transform.position.x;
			GameManager.getInstance ().targetLocation.y = gameObject.transform.position.y;

			// Indicate the islands that are now reachable
			ShowReachable ();
		}
	}

}
