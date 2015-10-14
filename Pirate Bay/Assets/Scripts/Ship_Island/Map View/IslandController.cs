using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IslandController : MonoBehaviour {
	public static bool hasRun = false;

	public Vector3 location;
	public List<IslandController> availableIslands;
	public int level;
	public bool cleared;
	
	
    void Awake() {

		Transform t = gameObject.GetComponent<Transform>();
		
		Vector3 location = new Vector3(t.position.x,t.position.y,t.position.z);

		Debug.Log (this.location);




    }

        void OnMouseUp() {
		//Debug.Log ( GameManager.getInstance().currentIsland == null);
		//Debug.Log ("available " + GameManager.getInstance().currentIsland.availableIslands);
		if (GameManager.getInstance().GetIsland(GameManager.getInstance().currentLocation).availableIslands.Contains (this)) {
			//Setting the persisted targetLocation to be below the new island so that the ship will move towards it.
			GameManager.getInstance().targetLocation.x = gameObject.transform.position.x;
			GameManager.getInstance().targetLocation.y = gameObject.transform.position.y - 1;

			//GameManager.getInstance().targetIsland = this;
		}
    }

}
