using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour {

	public float minSwipeDistY;
	
	public float minSwipeDistX;
	
	private Vector2 startPos;

	public Text show;

	private GameObject[] armoury;
	private int current;

	void start(){
		armoury = GameObject.FindGameObjectsWithTag ("Armour");
		current = 0;

		for (int i = 0; i <armoury.Length; i++) {
			if(i == 0){
				armoury[i].GetComponent<Renderer>().enabled= true;
			}else{
				armoury[i].GetComponent<Renderer>().enabled= false;
			}
		}


	}
	
	void Update()
	{
		//#if UNITY_ANDROID
		if (Input.touchCount > 0) 
			
		{
			
			Touch touch = Input.touches[0];
			
			
			
			switch (touch.phase) 
				
			{
				
			case TouchPhase.Began:
				
				startPos = touch.position;
				
				break;
				
				
				
			case TouchPhase.Ended:
				
				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
				
				if (swipeDistVertical > minSwipeDistY) 
					
				{
					
					float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
					
					if (swipeValue > 0){//up swipe
						show.text = "UP";
						//Jump ();
					}
					else if (swipeValue < 0){//down swipe
						show.text = "DOWN";
							//Shrink ();
					}
							
				}
				
				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
				
				if (swipeDistHorizontal > minSwipeDistX) 
					
				{
					
					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
					
					if (swipeValue > 0){//right swipe
						//show.text = "RIGHT";
						GameObject currentArmourShowing = null;

						foreach (GameObject g in armoury) {
							// if the game object is active then it is the currently showing crew member
							if(g.GetComponent<Renderer>().isVisible){
								currentArmourShowing = g;
							}
						}
						
						// index of the currently showing game object
						int indexOfCurrent = System.Array.IndexOf (armoury,currentArmourShowing);
						
						// assuming there are 2 or more crew. If the current crew is at index 1 or more.
						if (indexOfCurrent >= 1) {
							
							// disable current crew member
							currentArmourShowing.GetComponent<Renderer>().enabled= false;
							
							// enable the crew on the left 
							armoury [indexOfCurrent - 1].GetComponent<Renderer>().enabled= true;
						} else {
							
							// disable current crew member
							currentArmourShowing.GetComponent<Renderer>().enabled= false;
							
							// enable the crew on the right end
							armoury [armoury.Length-1].GetComponent<Renderer>().enabled= true;
						}


					}
						else if (swipeValue < 0){//left swipe
						//show.text = "LEFT";
							
					}
							
				}
				break;
			}
		}
	}

}
