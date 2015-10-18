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
	public GameObject LinePrefab;

    void Awake() {

		GameManager m = GameManager.getInstance ();

		Transform t = gameObject.GetComponent<Transform>();
        location = new Vector3(t.position.x, t.position.y, t.position.z);

		Debug.Log ("Initialised Island");

		// Draw Lock icon if island is not cleared
		if (!m.GetIslandStatus (location)) {
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

    }

	void OnMouseUp() {
		GameManager m = GameManager.getInstance ();
		// check if the island that the ship is currently at has been cleared.
		if (m.GetCurrentIslandStatus ()) {
			// Allow movement to island if current island is cleared
			setTarget();
		} else if(!m.GetCurrentIslandStatus () && m.GetIslandStatus(gameObject.GetComponent<Transform>().position)){
			// Allow movement if target island is cleared
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

		GameManager m = GameManager.getInstance ();
		if (m.GetIsland (m.targetLocation) == this) {
			DrawLines (1.0f);
		}
    }

	void DrawLock() {
		// Add lock icon over top of island to show that the player cannot go to it yet
		Instantiate(LockPrefab, location, Quaternion.identity);
	}

	void DrawLines(float alpha=0.2f) {
		Vector3 pos1 = this.transform.position;
		foreach (IslandController island in availableIslands) {

			Vector3 pos2 = island.transform.position;

			GameObject go = (GameObject)Instantiate (LinePrefab, location, Quaternion.identity);

			Quaternion rotation = Quaternion.LookRotation (pos1 - pos2, transform.TransformDirection (Vector3.up));
			go.transform.rotation = new Quaternion (0, 0, rotation.z, rotation.w);

			go.transform.position = Vector3.Lerp (pos1, pos2, 0.5f);

			Vector3 scale = new Vector3(1,1,1);
			scale.x = Vector3.Distance(pos1, pos2);

			go.transform.localScale = scale;

			SpriteRenderer sp = go.GetComponent<SpriteRenderer>();

			Color newColor = sp.color;
			newColor.a = alpha;
			sp.color = newColor;

		}
	}


	private void setTarget() {
		// sets the target to the island that is clicled on
		if (GameManager.getInstance ().GetIsland (GameManager.getInstance ().currentLocation).availableIslands.Contains (this)) {
			//Setting the persisted targetLocation to be below the new island so that the ship will move towards it.
			GameManager.getInstance ().targetLocation.x = gameObject.transform.position.x;
			GameManager.getInstance ().targetLocation.y = gameObject.transform.position.y;

			ShowReachable ();
		}
	}

}
