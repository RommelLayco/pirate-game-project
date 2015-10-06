using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class SelectCrewController : MonoBehaviour {


	public List<CrewMemberData> crewMembers;

	public Text name;
	public Text attack;
	public Text defense;
	public Text speed;
	private int crewIndex;

	// Use this for initialization
	void Start () {

		crewIndex = 0;
		// get list of current crew members
		crewMembers = GameObject.Find ("GameManager").GetComponent<GameManager> ().crewMembers;

		name.text = "Name: " + crewMembers [crewIndex].getName ();
		attack.text = "Attack: " + crewMembers [crewIndex].getAttack ();
		defense.text = "Defense: " + crewMembers [crewIndex].getDefense ();
		speed.text = "Speed: " + crewMembers [crewIndex].getSpeed ();


	}

	
	public void clickedLeft() {

		// if the current crew index is at 0 then go back to end of crew list
		if (crewIndex == 0) {

			crewIndex = crewMembers.Count - 1;
		
		} else {

			crewIndex = crewIndex - 1;
		}

		name.text = "Name: " + crewMembers [crewIndex].getName ();
		attack.text = "Attack: " + crewMembers [crewIndex].getAttack ();
		defense.text = "Defense: " + crewMembers [crewIndex].getDefense ();
		speed.text = "Speed: " + crewMembers [crewIndex].getSpeed ();


		/*

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
		*/
		
	}
	
	public void clickedRight() {

		// if the current crew index is at 0 then go back to end of crew list
		if (crewIndex == crewMembers.Count-1) {
			
			crewIndex = 0;
			
		} else {
			
			crewIndex = crewIndex + 1;
		}
		
		name.text = "Name: " + crewMembers [crewIndex].getName ();
		attack.text = "Attack: " + crewMembers [crewIndex].getAttack ();
		defense.text = "Defense: " + crewMembers [crewIndex].getDefense ();
		speed.text = "Speed: " + crewMembers [crewIndex].getSpeed ();


		/*
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
			*/
	}

}
