using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float roomStartDelay = 2f;
    public static GameManager instance = null;

    private BoardManager boardScript;
    private Text roomText;
    private GameObject roomImage;
    private bool doingSetup;
    private int roomNumber;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            instance = this;
        }

        
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        boardScript = GetComponent<BoardManager>();

        //build the room
        InitGame();
    }

    void OnLevelWasLoaded(int index)
    {
      
        roomNumber++; 
        InitGame();
    }

    //Build the room
    void InitGame()
    {

        //While doingSetup is true the player can't move, prevent player from moving while title card is up.
        doingSetup = true;

        
        roomImage = GameObject.Find("RoomImage");
        roomText = GameObject.Find("RoomText").GetComponent<Text>();
        roomText.text = "Room " + roomNumber;
        roomImage.SetActive(true);

        Invoke("HideRoomImage", roomStartDelay);

        boardScript.SetupScene();

    }

    void HideRoomImage()
    {
        roomImage.SetActive(false);
        doingSetup = false;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
