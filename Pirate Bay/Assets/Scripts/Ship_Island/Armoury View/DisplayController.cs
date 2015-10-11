using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour {

	public GameObject armour;	
	private List<Armour> armoury;

	public GameObject empty;

	public Text textPrefab;
	

	private int x;
	private int y;
	
	// Use this for initialization
	void Start () {

		x = -197;
		y = 97;

		armoury = GameObject.Find ("GameManager").GetComponent<GameManager> ().armoury;

		//Debug.Log ("" + armoury.Count);


		// add the armour
		for (int i=0; i < armoury.Count ; i++) {



			GameObject temp = Instantiate(armour) as GameObject;

			temp.transform.position = new Vector3(x,y,0);

			temp.transform.SetParent(gameObject.transform,false);


			Text t = (Text)Instantiate(textPrefab,new Vector3(x,y,0),Quaternion.identity);

			t.transform.SetParent(gameObject.transform);

			//GameObject t = new GameObject();
			//t.AddComponent<TextMesh>();
			//t.GetComponent<TextMesh>().text = "WATUP";
			//t.GetComponent<TextMesh>().fontSize = 5;
			//t.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, 0);
			//t.transform.SetParent(temp.transform,true);


			x = x + 150;

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
