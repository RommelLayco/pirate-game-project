﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IslandController : MonoBehaviour {
	public static bool hasRun = false;

	public Vector3 location;
	public List<IslandController> availableIslands;
	public int level;
	public string rival;
	
	
    void Awake() {

		Transform t = gameObject.GetComponent<Transform>();
        Vector3 location = new Vector3(t.position.x,t.position.y,t.position.z);
        Debug.Log (this.location);

		Debug.Log ("Island status: " + GameManager.getInstance ().GetCurrentIslandStatus ());

    }

        void OnMouseUp() {
		//Debug.Log ( GameManager.getInstance().currentIsland == null);
		//Debug.Log ("available " + GameManager.getInstance().currentIsland.availableIslands);

		// check if the island that the ship is currently at has been cleared.
		if (GameManager.getInstance ().GetCurrentIslandStatus ()) {
			setTarget();
		} else {

			// if the game object island that is to be clicked on is cleared 
			// then set the target
			if(GameManager.getInstance().GetIslandStatus(gameObject.GetComponent<Transform>().position)){
				
			
			}else{
				Debug.Log("Sorry, but you have not got access to this island");
			}
		}
    }
    public void ShowReachable()
    {
        GameObject[] islands = GameObject.FindGameObjectsWithTag("Island");

        foreach (GameObject g in islands)
        {

            g.GetComponent<SpriteRenderer>().color = new Color(0.5F, 0.5F, 0.5F);// 434343FF;
        }

        this.GetComponent<SpriteRenderer>().color = new Color(1F, 1F, 1F);
        foreach (IslandController ic in this.availableIslands)
        {

            ic.GetComponent<SpriteRenderer>().color = new Color(1F, 1F, 1F);

        }
    }

	private void setTarget(){
		// sets the target to the island that is clicled on
		if (GameManager.getInstance ().GetIsland (GameManager.getInstance ().currentLocation).availableIslands.Contains (this)) {
			//Setting the persisted targetLocation to be below the new island so that the ship will move towards it.
			ShowReachable ();
			
			GameManager.getInstance ().targetLocation.x = gameObject.transform.position.x;
			GameManager.getInstance ().targetLocation.y = gameObject.transform.position.y - 1;
			
			GameManager.getInstance ().SetCurrentIslandStatus (true);
			
		}
	}
}
