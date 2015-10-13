using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IslandController : MonoBehaviour {
    private GameManager manager;
	public List<GameObject> reachable;
	public static bool hasRun = false;

	public Vector3 location;
	public List<IslandController> availableIslands = new List<IslandController>();
	public int level;
	public bool cleared;
	
	
    void Awake() {

		manager = GameManager.getInstance();

		Transform t = gameObject.GetComponent<Transform>();
		
		Vector3 location = new Vector3(t.position.x,t.position.y,t.position.z);

		Debug.Log (this.location);

		manager.islands.Add(this);

		Debug.Log ("manager islands " + manager.islands.Count);

		foreach (GameObject g in reachable) {
			this.availableIslands.Add(g.GetComponent<IslandController>());
		}



    }
        void OnMouseUp() {
		//Debug.Log ( manager.currentIsland == null);
		//Debug.Log ("available " + manager.currentIsland.availableIslands);
		if (manager.currentIsland.availableIslands.Contains (this)) {
			//Setting the persisted targetLocation to be below the new island so that the ship will move towards it.
			manager.targetLocation.x = gameObject.transform.position.x;
			manager.targetLocation.y = gameObject.transform.position.y - 1;
			manager.targetIsland = this;
		}
    }

}
