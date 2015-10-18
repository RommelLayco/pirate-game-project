using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Island  {

	public Vector3 location;
	public List<Island> availableIsland;
	public int level;
	public bool cleared;


	public Island(Vector3 location, int level){
		this.location = location;
		this.availableIsland = new List<Island> ();
		this.level = level;
		this.cleared = false;

		//Debug.Log("location " + location  + ", level " + level);
	}
    

}
