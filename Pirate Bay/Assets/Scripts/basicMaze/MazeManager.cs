using UnityEngine;
using System.Collections;

public class MazeManager : MonoBehaviour {

    public static MazeManager instance = null;
         
    public BoardManager boardScript;

	// Use this for initialization
	void Awake ()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
        InitGame();
	}
	
    void InitGame()
    {
        boardScript.SetUpScene();

    }

	// Update is called once per frame
	void Update () {
	
	}
}
