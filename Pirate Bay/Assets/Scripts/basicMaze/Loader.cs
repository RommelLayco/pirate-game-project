using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject mazeManager;

	// Use this for initialization
	void Awake ()
    {
	    if(MazeManager.instance == null)
        {
            Instantiate(mazeManager);
        }    
	}

}
