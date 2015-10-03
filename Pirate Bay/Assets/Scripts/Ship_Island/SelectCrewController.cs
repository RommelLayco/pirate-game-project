using UnityEngine;
using System.Collections;

public class SelectCrewController : MonoBehaviour {

	private GameObject[] crew;
	
	// Use this for initialization
	void Start () {
		crew = GameObject.FindGameObjectsWithTag("Crew");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void clickedLeft() {
		
		GameObject curentCrewShowing = null;
		
		foreach (GameObject g in crew) {
			// if the game object is active then it is the currently showing crew member
			if(g.activeSelf){
				curentCrewShowing = g;
			}
		}
		
		int indexOfCurrent = System.Array.IndexOf (crew,curentCrewShowing);
		
		if (indexOfCurrent >= 1) {
			
			// disable current crew member
			curentCrewShowing.SetActive (false);
			
			// enable the crew on the left 
			crew [indexOfCurrent - 1].SetActive (true);
		} else {
			
			// disable current crew member
			curentCrewShowing.SetActive (false);
			
			// enable the crew on the right end
			crew [crew.Length-1].SetActive (true);
		}
		
		
	}
	
	public void clickedRight() {
		
		GameObject curentCrewShowing = null;
		
		foreach (GameObject g in crew) {
			// if the game object is active then it is the currently showing crew member
			if(g.activeSelf){
				curentCrewShowing = g;
			}
		}
		
		
		int indexOfCurrent = System.Array.IndexOf (crew,curentCrewShowing);
		
		if (indexOfCurrent <= crew.Length-2) {
			
			// disable current crew member
			curentCrewShowing.SetActive (false);
			
			// enable the crew on the left 
			crew [indexOfCurrent + 1].SetActive (true);
		} else {
			
			// disable current crew member
			curentCrewShowing.SetActive (false);
			
			// enable the crew on the right end
			crew [0].SetActive (true);
		}
		
	}
}
