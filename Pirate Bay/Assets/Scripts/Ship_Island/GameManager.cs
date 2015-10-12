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
    public float[] sailsSpeed = { .125f, 0.25f, .5f, 0.75f, 1 };

    //CannonRoom
    public int cannonLevel = 1;
    public int[] cannonLevels = { 1, 2, 3, 4, 5 };
    public int[] cannonCosts = { 100, 200, 300, 400, 500 };
    public int[] cannonDamage = { 5, 10, 20, 50, 100 };

    //Hull
    public int hullLevel = 1;
    public int[] hullCosts = { 100, 200, 300, 400, 500 };
    public int[] hullHealth = { 50, 100, 200, 500, 1000 };

    //General
    public int gold = 1000;
    public int crewSize;
    public int crewMax;
    public List<CrewMemberData> crewMembers = new List<CrewMemberData>();
    public List<CrewMemberData> explorers = new List<CrewMemberData>();

    public List<Armour> armoury = new List<Armour>();
	public List<Weapon> weapons = new List<Weapon>();

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
        InitialiseShip();
        crewSize = crewMembers.Count;
        crewMax = bunkCapacities[bunkLevel - 1];

        
    }

    void Update() {
        crewMax = bunkCapacities[bunkLevel - 1];
        crewSize = crewMembers.Count;
    }

    private void InitialiseShip()
    {
        sailsLevel = 4;
        cannonLevel = 4;
        hullLevel = 4;
    }
    private void initialiseCrew() {
        crewMembers.Add(new CrewMemberData("Luke Woly", 10, 3, 10, null, null));
        crewMembers.Add(new CrewMemberData("Daniel Brocx", 9001, 9001, 1, null, null));

		armoury.Add (new Armour (100, "Red armour", new CrewMemberData ("Anmol Desai", 10, 3, 10, null, null)));
		armoury.Add (new Armour (100, "Black armour", new CrewMemberData ("Anmol Desai", 10, 3, 10, null, null)));

		weapons.Add(new Weapon(100,"Sword",crewMembers[0]));


    }
}
