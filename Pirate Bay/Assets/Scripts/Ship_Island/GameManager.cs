using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;

    //IslandView Data
    public Vector3 targetLocation = new Vector3(-500, -500, -500);
    public Vector3 currentLocation = new Vector3(-500, -500, -500);

    //BunkRoom
    public int bunkLevel = 1;
    public int[] bunkLevels = { 1, 2, 3, 4, 5 };//not sure if this is necessary
    public int[] bunkCosts = { 100, 200, 300, 400, 500 };//Need to change these once gold is implemented
    public int[] bunkCapacities = { 2, 4, 6, 8, 10 };

    //SailsRoom
    public int sailsLevel = 1;
    public int maxSails = 5;
    public int[] sailsCosts = { 100, 200, 300, 400, 500 };
    public float[] sailsSpeed = { 1.0f, 1.5f, 2, 3, 5 };

    //CannonRoom
    public int cannonLevel = 1;
    public int[] cannonLevels = { 1, 2, 3, 4, 5 };
    public int[] cannonCosts = { 100, 200, 300, 400, 500 };
    public int[] cannonDamage = { 5, 10, 20, 50, 100 };

    //General
    public int gold = 1000;
    public int crewSize;
    public int crewMax;
    public List<CrewMemberData> crewMembers = new List<CrewMemberData>();
    public List<CrewMemberData> explorers = new List<CrewMemberData>();

    //Hire/Fire
    public int crewIndex = 0;
    public int hireCost = 200;

    public static GameManager getInstance() {
        if (_instance == null) {
            GameObject g = new GameObject();
            _instance = g.AddComponent<GameManager>();
            //_instance.Start();
        }
        return _instance;

    }

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {
        //Initialising all relevant variables.
        initialiseCrew();

        crewSize = crewMembers.Count;
        crewMax = bunkCapacities[bunkLevel - 1];
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
