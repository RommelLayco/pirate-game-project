using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour {

	public GameObject armour;	
	private List<Armour> armoury;

	public GameObject empty;

	public Text textPrefab;

	public GameObject weapon;

	private List<Weapon> weapons;
	

	private int x;
	private int y;
	
	// Use this for initialization
	void Start () {

		x = -197;
		y = 97;

		armoury = GameManager.getInstance().armoury;
		weapons = GameManager.getInstance ().weapons;

		//Debug.Log ("" + armoury.Count);


		// need to add if statement to differentiate between showing armour or weapons on panel



		if (true) {

			// add the armour
			for (int i=0; i < armoury.Count; i++) {



				GameObject temp = Instantiate (armour) as GameObject;

				temp.transform.position = new Vector3 (x, y, 0);

				temp.transform.SetParent (gameObject.transform, false);


				Text t = (Text)Instantiate (textPrefab, new Vector3 (temp.transform.position.x, temp.transform.position.y, 0), Quaternion.identity);


				t.transform.SetParent (gameObject.transform);
				t.transform.localScale = new Vector3 (1, 1, 1);

				t.transform.position = temp.transform.position + new Vector3 (2.95f, 1.0f, 0f);

				// set the text to the value in the armour
				t.text = armoury [i].getStrength ().ToString ();


				//Debug.Log ("temp pos: " + temp.transform.position);
				//Debug.Log ("t pos : " + t.transform.position);


				// shift the generating sprites along x axis
				x = x + 150;

			}

		}


		if (false) {

			// add the weapons
			for (int i=0; i < weapons.Count; i++) {
				
				
				
				GameObject temp = Instantiate (weapon) as GameObject;
				
				temp.transform.position = new Vector3 (x, y, 0);
				
				temp.transform.SetParent (gameObject.transform, false);
				
				
				Text t = (Text)Instantiate (textPrefab, new Vector3 (temp.transform.position.x, temp.transform.position.y, 0), Quaternion.identity);
				
				
				t.transform.SetParent (gameObject.transform);
				t.transform.localScale = new Vector3 (1, 1, 1);
				
				t.transform.position = temp.transform.position + new Vector3 (2.95f, 1.0f, 0f);
				
				// set the text to the value in the armour
				t.text = weapons [i].getStrength ().ToString ();
				
				
				//Debug.Log ("temp pos: " + temp.transform.position);
				//Debug.Log ("t pos : " + t.transform.position);
				
				
				// shift the generating sprites along x axis
				x = x + 150;
				
			}
		
		}



		// add the empty blocks
		for (int i=0; i < armoury.Count ; i++) {
			
			
			
			GameObject temp = Instantiate(empty) as GameObject;
			
			temp.transform.position = new Vector3(x,y,0);
			
			temp.transform.SetParent(gameObject.transform,false);
			
			x = x + 150;
			
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
