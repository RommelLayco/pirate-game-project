using UnityEngine;
using System.Collections;

public class SelectCrewController : MonoBehaviour {

	private GameObject[] crew;
	
	// Use this for initialization
	void Start () {
		crew = GameObject.FindGameObjectsWithTag("Crew");
	

		for (int i = 0; i<crew.Length; i++) {

			if(i == 0){
				crew[i].GetComponent<Renderer>().enabled= true;
			}else{
				crew[i].GetComponent<Renderer>().enabled = false;
			}
		}
	


	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void clickedLeft() {

		Debug.Log (crew.Length);

		// set current object showing to null
		GameObject curentCrewShowing = null;
		
		foreach (GameObject g in crew) {
			// if the game object is active then it is the currently showing crew member
			if(g.GetComponent<Renderer>().isVisible){
				curentCrewShowing = g;
			}
		}

		// index of the currently showing game object
		int indexOfCurrent = System.Array.IndexOf (crew,curentCrewShowing);

		// assuming there are 2 or more crew. If the current crew is at index 1 or more.
		if (indexOfCurrent >= 1) {
			
			// disable current crew member
			curentCrewShowing.GetComponent<Renderer>().enabled= false;

			// enable the crew on the left 
			crew [indexOfCurrent - 1].GetComponent<Renderer>().enabled= true;
		} else {

			Debug.Log(crew.Length);

			// disable current crew member
			curentCrewShowing.GetComponent<Renderer>().enabled= false;
			
			// enable the crew on the right end
			crew [crew.Length-1].GetComponent<Renderer>().enabled= true;
		}
		
		
	}
	
	public void clickedRight() {
		
		GameObject curentCrewShowing = null;
		
		foreach (GameObject g in crew) {
			// if the game object is active then it is the currently showing crew member
			if(g.GetComponent<Renderer>().isVisible){
				curentCrewShowing = g;
			}
		}
		
		
		int indexOfCurrent = System.Array.IndexOf (crew,curentCrewShowing);
		
		if (indexOfCurrent <= crew.Length - 2) {
			
			// disable current crew member
			curentCrewShowing.GetComponent<Renderer> ().enabled = false;
			
			// enable the crew on the left 
			crew [indexOfCurrent + 1].GetComponent<Renderer> ().enabled = true;
		} else {
			
			// disable current crew member
			curentCrewShowing.GetComponent<Renderer> ().enabled = false;
			
			// enable the crew on the right end
			crew [0].GetComponent<Renderer> ().enabled = true;
		}		
	}
}
