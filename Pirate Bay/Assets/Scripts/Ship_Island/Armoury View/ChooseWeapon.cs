using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ChooseWeapon : MonoBehaviour {

	private int numberOfTouches;
	
	private List<Weapon> weapons;

	
	
	// Use this for initialization
	void Start () {
		numberOfTouches = 0;
		weapons = GameManager.getInstance().weapons;
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
		}
	}

	
	
	void clicked() {
		GameObject currentShowingWeapon = GameObject.FindGameObjectWithTag ("Weapon");
		currentShowingWeapon.GetComponent<SpriteRenderer> ().sprite = gameObject.GetComponent<SpriteRenderer> ().sprite;
	}
}
