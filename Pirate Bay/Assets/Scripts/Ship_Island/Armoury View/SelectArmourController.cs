using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectArmourController : MonoBehaviour {
    private GameManager manager;

    private GameObject panel = null;
    private Image image;


    // Use this for initialization
    void Awake() {
        manager = GameManager.getInstance();
    }
    void Start() {

        panel = GameObject.FindGameObjectWithTag("Panel");
        image = panel.GetComponent<Image>();

        // this makes the panel not visible
        Image[] images = panel.GetComponentsInChildren<Image>();
        foreach (Image r in images) {
            r.enabled = false;
        }

        // make all the sprites inivisible
        Renderer[] renderers = panel.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) {
            r.enabled = false;
        }

        // set any text to invisible when panel is shown
        Text[] texts = panel.GetComponentsInChildren<Text>();
        foreach (Text r in texts) {
            r.enabled = false;
        }
        image.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        foreach (Touch t in Input.touches) {
            if (t.phase == TouchPhase.Ended) {
                bool contained = gameObject.GetComponent<Collider>().bounds.Contains(t.position);
                if (contained) {
                    clicked();
                }
            }
        }
    }

    void OnMouseDown() {
        clicked();
    }

    // when the armour image is clicked...make everything visible
    void clicked() {
        //Need to get the panel then call get component on that

        GameObject.Find("SelectPanel").GetComponent<DisplayController>().armourClicked();

        image.enabled = true;

        Image[] images = panel.GetComponentsInChildren<Image>();
        foreach (Image r in images) {
            r.enabled = true;
        }

        Renderer[] renderers = panel.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) {
            r.enabled = true;
        }

        // set text to true
        Text[] texts = panel.GetComponentsInChildren<Text>();
        foreach (Text r in texts) {
            r.enabled = true;
        }


    }

    public void clickedTop() {

        /*
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
	
		*/

    }

    public void clickedBottom() {

        /*
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
		*/
    }
}
