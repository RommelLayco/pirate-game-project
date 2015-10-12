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

		armoury = GameManager.getInstance().armoury;

		//Debug.Log ("" + armoury.Count);


		// add the armour
		for (int i=0; i < armoury.Count ; i++) {



			GameObject temp = Instantiate(armour) as GameObject;

			temp.transform.position = new Vector3(x,y,0);

			temp.transform.SetParent(gameObject.transform,false);


			Text t = (Text)Instantiate(textPrefab,new Vector3(temp.transform.position.x,temp.transform.position.y,0),Quaternion.identity);


			t.transform.SetParent(gameObject.transform);
			t.transform.localScale = new Vector3(1,1,1);

			t.transform.position = temp.transform.position+ new Vector3(2.95f,0.95f,0f);

			//t.transform.position = new Vector3(t.transform.position.x + 20,t.transform.position.y + 20,0);
			t.text = "hey";


			Debug.Log ("temp pos: " + temp.transform.position);
			Debug.Log ("t pos : " + t.transform.position);

			//t.transform.position = temp.transform.position;

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
