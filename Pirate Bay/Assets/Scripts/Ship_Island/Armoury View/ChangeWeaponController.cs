using UnityEngine;
using System.Collections;

public class ChangeWeaponController : MonoBehaviour {
	

	// Use this for initialization
	void Start () {

		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void clickedTop() {

		/*
		GameObject curentWeaponShowing = null;
		
		foreach (GameObject g in weapons) {
			// if the game object is active then it is the currently showing crew member
			if(g.GetComponent<Renderer>().isVisible){
				curentWeaponShowing = g;
			}
		}
		
		int indexOfCurrent = System.Array.IndexOf (weapons,curentWeaponShowing);
		
		// assuming there are 2 or more armour. If the current armour is at index 1 or more.
		if (indexOfCurrent >= 1) {
			
			// disable current crew member
			curentWeaponShowing.GetComponent<Renderer>().enabled= false;
			
			// enable the crew on the left 
			weapons [indexOfCurrent - 1].GetComponent<Renderer>().enabled= true;
		} else {
			
			// disable current crew member
			curentWeaponShowing.GetComponent<Renderer>().enabled= false;
			
			// enable the crew on the right end
			weapons [weapons.Length-1].GetComponent<Renderer>().enabled= true;
		}
		*/
		
		
	}
	
	public void clickedBottom() {

		/*
		GameObject curentWeaponShowing = null;
		
		foreach (GameObject g in weapons) {
			// if the game object is active then it is the currently showing crew member
			if(g.GetComponent<Renderer>().isVisible){
				curentWeaponShowing = g;
			}
		}
		
		
		int indexOfCurrent = System.Array.IndexOf (weapons,curentWeaponShowing);
		
		if (indexOfCurrent <= weapons.Length - 2) {
			
			// disable current crew member
			curentWeaponShowing.GetComponent<Renderer> ().enabled = false;
			
			// enable the crew on the left 
			weapons [indexOfCurrent + 1].GetComponent<Renderer> ().enabled = true;
		} else {
			
			// disable current crew member
			curentWeaponShowing.GetComponent<Renderer> ().enabled = false;
			
			// enable the crew on the right end
			weapons [0].GetComponent<Renderer> ().enabled = true;
		}		
		*/
	}
}
