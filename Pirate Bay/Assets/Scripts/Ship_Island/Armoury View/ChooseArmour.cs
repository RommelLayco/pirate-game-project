using UnityEngine;
using System.Collections;

public class ChooseArmour : MonoBehaviour {

	private int numberOfTouches;

	// Use this for initialization
	void Start () {
		numberOfTouches = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		foreach (Touch t in Input.touches)
		{

			if (t.phase == TouchPhase.Ended)
			{
				if(t.tapCount == 2){
					bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);
					if(contained){
						clicked();
					}
				}

				if(t.tapCount == 1){
					displayInfoAboutArmour();
				}

			}
			
		}
		
	}
	
	void OnMouseDown(){
		numberOfTouches = numberOfTouches + 1;
		
		if (numberOfTouches == 2) {
			clicked ();
			numberOfTouches = 0;
		}
	}

	void displayInfoAboutArmour(){
		
	}
	
	
	void clicked() {
		GameObject currentShowingArmour = GameObject.FindGameObjectWithTag ("Armour");
		currentShowingArmour.GetComponent<SpriteRenderer> ().sprite = gameObject.GetComponent<SpriteRenderer> ().sprite;
	}
}
