using UnityEngine;
using System.Collections;

public class SelectArmourController : MonoBehaviour {

	private GameObject[] armoury;
	
	// Use this for initialization
	void Start () {
		armoury = GameObject.FindGameObjectsWithTag("Armour");

		for (int i = 0; i<armoury.Length; i++) {
			
			if(i == 0){
				armoury[i].GetComponent<Renderer>().enabled= true;
			}else{
				armoury[i].GetComponent<Renderer>().enabled = false;
			}
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void clickedTop() {
		
		GameObject curentArmourShowing = null;
		
		foreach (GameObject g in armoury) {
			// if the game object is active then it is the currently showing crew member
			if(g.GetComponent<Renderer>().isVisible){
				curentArmourShowing = g;
			}
		}
		
		int indexOfCurrent = System.Array.IndexOf (armoury,curentArmourShowing);

		// assuming there are 2 or more armour. If the current armour is at index 1 or more.
		if (indexOfCurrent >= 1) {
			
			// disable current crew member
			curentArmourShowing.GetComponent<Renderer>().enabled= false;
			
			// enable the crew on the left 
			armoury [indexOfCurrent - 1].GetComponent<Renderer>().enabled= true;
		} else {
			
			// disable current crew member
			curentArmourShowing.GetComponent<Renderer>().enabled= false;
			
			// enable the crew on the right end
			armoury [armoury.Length-1].GetComponent<Renderer>().enabled= true;
		}

		
		
	}
	
	public void clickedBottom() {
		
		GameObject curentArmourShowing = null;
		
		foreach (GameObject g in armoury) {
			// if the game object is active then it is the currently showing crew member
			if(g.GetComponent<Renderer>().isVisible){
				curentArmourShowing = g;
			}
		}
		
		
		int indexOfCurrent = System.Array.IndexOf (armoury,curentArmourShowing);

		if (indexOfCurrent <= armoury.Length - 2) {
			
			// disable current crew member
			curentArmourShowing.GetComponent<Renderer> ().enabled = false;
			
			// enable the crew on the left 
			armoury [indexOfCurrent + 1].GetComponent<Renderer> ().enabled = true;
		} else {
			
			// disable current crew member
			curentArmourShowing.GetComponent<Renderer> ().enabled = false;
			
			// enable the crew on the right end
			armoury [0].GetComponent<Renderer> ().enabled = true;
		}		
		
	}
}
