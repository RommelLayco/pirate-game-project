using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChooseArmour : MonoBehaviour {

	private int numberOfTouches;

	private List<Armour> armoury;

	//public Text strength;
	//public Text name;


	// Use this for initialization
	void Start () {
		numberOfTouches = 0;
		armoury = GameObject.Find ("GameManager").GetComponent<GameManager> ().armoury;
		displayInfoAboutArmour();
	}
	
	// Update is called once per frame
	void Update () {
	
		foreach (Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Ended){

				if(t.tapCount == 2){
						bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);

						if(contained){
							clicked();
						}
					}

			}
			
		}
		
	}
	
	void OnMouseDown(){
		numberOfTouches = numberOfTouches + 1;
		
		if (numberOfTouches == 2) {
			clicked ();
			numberOfTouches = 0;
			displayInfoAboutArmour();
		}
	}

	void displayInfoAboutArmour(){

		// if the game object has tag Armour1 then show stats of index 0 in armoury
		//if(gameObject.tag == "Armour1") {
		//	strength.text = "Strength: " + armoury[0].getStrength();
		//	name.text = "Name: " + armoury[0].getName();
		//}

	}
	
	
	void clicked() {
		GameObject currentShowingArmour = GameObject.FindGameObjectWithTag ("Armour");
		currentShowingArmour.GetComponent<SpriteRenderer> ().sprite = gameObject.GetComponent<SpriteRenderer> ().sprite;
	}
}
