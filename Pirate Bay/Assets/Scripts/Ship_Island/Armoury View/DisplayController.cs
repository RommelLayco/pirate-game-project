using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayController : MonoBehaviour {

	public GameObject armour;	
	private List<Armour> armoury;

	public GameObject empty;

	private int x;
	private int y;
	
	// Use this for initialization
	void Start () {

		x = -197;
		y = 97;

		armoury = GameObject.Find ("GameManager").GetComponent<GameManager> ().armoury;

		//Debug.Log ("" + armoury.Count);

		for (int i=0; i < armoury.Count ; i++) {



			GameObject temp = Instantiate(armour) as GameObject;

			temp.transform.position = new Vector3(x,y,0);

			temp.transform.SetParent(gameObject.transform,false);

			x = x + 150;

		}

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
