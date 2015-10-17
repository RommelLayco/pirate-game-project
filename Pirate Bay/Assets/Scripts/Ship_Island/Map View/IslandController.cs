using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IslandController : MonoBehaviour {

	public Vector3 location;
	public List<IslandController> availableIslands;
	public int level;
	public string rival;
	public GameObject LockPrefab;
	public bool status;

    void Awake() {

		GameManager m = GameManager.getInstance ();

		Transform t = gameObject.GetComponent<Transform>();
        location = new Vector3(t.position.x, t.position.y, t.position.z);

		status = m.GetIslandStatus (location);

		Debug.Log ("Initialised Island");

		//if (!m.GetIslandStatus (location) && (this != m.GetIsland(m.currentLocation))) {
		if (!m.GetIslandStatus (location)) {
			DrawLock();
		}

		//gameObject.SetActive (true);

    }

	void OnMouseUp() {
		GameManager m = GameManager.getInstance ();
		// check if the island that the ship is currently at has been cleared.
		if (m.GetCurrentIslandStatus ()) {
			setTarget();
		} else if(!m.GetCurrentIslandStatus () && m.GetIslandStatus(gameObject.GetComponent<Transform>().position)){
			setTarget();
		} else {
			Debug.Log("Sorry, but you have not got access to this island");
		}
    }

    public void ShowReachable() {
        GameObject[] islands = GameObject.FindGameObjectsWithTag("Island");

        foreach (GameObject g in islands) {
            g.GetComponent<SpriteRenderer>().color = new Color(0.5F, 0.5F, 0.5F);// 434343FF;
        }

        this.GetComponent<SpriteRenderer>().color = new Color(1F, 1F, 1F);
        foreach (IslandController ic in this.availableIslands) {
            ic.GetComponent<SpriteRenderer>().color = new Color(1F, 1F, 1F);
        }
    }

	void DrawLock() {
		// Add lock icon over top of island to show that the player cannot go to it yet
		Instantiate(LockPrefab, location, Quaternion.identity);
	}

	private void setTarget() {
		// sets the target to the island that is clicled on
		if (GameManager.getInstance ().GetIsland (GameManager.getInstance ().currentLocation).availableIslands.Contains (this)) {
			//Setting the persisted targetLocation to be below the new island so that the ship will move towards it.
			ShowReachable ();
			
			GameManager.getInstance ().targetLocation.x = gameObject.transform.position.x;
			//GameManager.getInstance ().targetLocation.y = gameObject.transform.position.y - 1;
			GameManager.getInstance ().targetLocation.y = gameObject.transform.position.y;

		}
	}

}
