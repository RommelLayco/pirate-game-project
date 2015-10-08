using UnityEngine;
using System.Collections;

public class ChooseArmour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		foreach (Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Ended)
			{
				
				bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);
				if(contained)
					clicked();
			}
			
		}
		
	}
	
	void OnMouseDown(){
		clicked ();
	}
	
	
	void clicked() {
		GameObject currentShowingArmour = GameObject.FindGameObjectWithTag ("Armour");
		currentShowingArmour.GetComponent<SpriteRenderer> ().sprite = gameObject.GetComponent<SpriteRenderer> ().sprite;
	}
}
