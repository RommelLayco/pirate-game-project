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

    //General
    public int crewSize;
    public int crewMax;
    public List<CrewMemberData> crewMembers = new List<CrewMemberData>();

    //Hire/Fire
    public int crewIndex;

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

        bunkLevel = 1;
        crewMembers.Add(new CrewMemberData("Luke Woly", 10, 3, 10, null, null));
        crewMembers.Add(new CrewMemberData("Daniel Brocx", 9001, 9001, 1, null, null));
        crewSize = crewMembers.Count;
        crewMax = bunkCapacities[bunkLevel - 1];
        crewIndex = 0;
    }

    void Update() {
        crewMax = bunkCapacities[bunkLevel - 1];
        crewSize = crewMembers.Count;
    }
}
