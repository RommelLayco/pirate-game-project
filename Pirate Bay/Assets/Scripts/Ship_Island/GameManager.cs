using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    //IslandView Data
    public Vector3 targetLocation;
    public Vector3 currentLocation;

    //BunkRoom
    public int bunkLevel;
    public int[] bunkLevels = { 1, 2, 3, 4, 5 };//not sure if this is necessary
    public int[] bunkCosts = { 100, 200, 300, 400, 500 };//Need to change these once gold is implemented
    public int[] bunkCapacities = { 2, 4, 6, 8, 10 };

    //CannonRoom
	public int cannonLevel;
	public int[] cannonLevels = { 1, 2, 3, 4, 5 };
    public int[] cannonCosts = { 100, 200, 300, 400, 500 };
    public int[] cannonDamage = { 5, 10, 20, 50, 100 };

    //General
    public int gold;
    public int crewSize;
    public int crewMax;
    public List<CrewMemberData> crewMembers = new List<CrewMemberData>();
    public List<CrewMemberData> explorers = new List<CrewMemberData>();

    //Hire/Fire
    public int crewIndex;

    public static GameManager getInstance() {
        if (!_instance) {
            _instance = new GameManager();
            _instance.Start();
        }
        return _instance;

    }

    void Awake() {
        //if we don't have an GameManager set yet
        if (!_instance) {
            _instance = this;
            //otherwise, if we do, kill this instance
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {
        //Initialising all relevant variables.
        targetLocation = new Vector3(-500, -500, -500);
        currentLocation = new Vector3(-500, -500, -500);

        initialiseCrew();

        gold = 500;

        bunkLevel = 1;
        cannonLevel = 1;

        crewSize = crewMembers.Count;
        crewMax = bunkCapacities[bunkLevel - 1];
        crewIndex = 0;
    }

    void Update() {
        crewMax = bunkCapacities[bunkLevel - 1];
        crewSize = crewMembers.Count;
    }


    private void initialiseCrew() {
        crewMembers.Add(new CrewMemberData("Luke Woly", 10, 3, 10, null, null));
        crewMembers.Add(new CrewMemberData("Daniel Brocx", 9001, 9001, 1, null, null));
    }
}
