using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public Vector3 targetLocation;
	public Vector3 currentLocation;


	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		targetLocation = new Vector3(-500,-500,-500);
		currentLocation = new Vector3(-500,-500,-500);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
