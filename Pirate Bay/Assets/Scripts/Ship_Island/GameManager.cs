using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //Captain's Data
    public string captainName = "BlackBeard";
    public int notoriety = 200;
    public int IslandsCleared = 0;

    //IslandView Data
    public Vector3 targetLocation = new Vector3(-500, -500, -500);
    public Vector3 currentLocation = new Vector3(-500, -500, -500);

    //BunkRoom
    public int bunkLevel = 1;
    public int[] bunkCosts = { 100, 200, 300, 400, 500 };//Need to change these once gold is implemented
    public int[] bunkCapacities = { 2, 4, 6, 8, 10 };

    //SailsRoom
    public int sailsLevel = 1;
    public int[] sailsCosts = { 100, 200, 300, 400, 500 };
    public float[] sailsSpeed = { .25f, 0.4f, .55f, .7f, 1.0f };

    //CannonRoom
    public int cannonLevel = 1;
    public int[] cannonCosts = { 100, 200, 300, 400, 500 };
    public int[] cannonDamage = { 20, 30, 40, 60, 80 };

    //Hull
    public int hullLevel = 1;
    public int[] hullCosts = { 100, 200, 300, 400, 500 };
    public int[] hullHealth = { 200, 350, 500, 650, 800 };

    //General
    public int maxLevel = 5;
    public int gold = 1000;
    public int crewSize;
    public int crewMax;
    public List<CrewMemberData> crewMembers = new List<CrewMemberData>();
    public List<CrewMemberData> explorers = new List<CrewMemberData>();
    public int[] levelBoundaries = { 150, 500, 1250, 2400, 4000 };

    public List<Armour> armoury = new List<Armour>();
    public List<Weapon> weapons = new List<Weapon>();
    public Equipment selectedEquipment = null;

    //Hire/Fire
    public int crewIndex = 0;
    public int hireCost = 200;

    public int blueRivalry=1;
    public int redRivalry=1;
    public int whiteRivalry=1;

    //player position in maze
    public int islandLevel = 0;
	public string islandRival = "";
    public Vector3 playerPos = new Vector3(0, 0, 0f);
    public bool inMaze = false;
    public List<Vector3> collectedgold = new List<Vector3>();
    public int mazeGold = 0;
    public int seed = 0;
    // Island view

    //Acheievement system
    public List<Achievement> achievements;
    public int achievementIndex = 0;
    // Dictionary that maps islands to their cleared status
    public List<KeyValuePair<Vector3, bool>> IslandClearedStatus = new List<KeyValuePair<Vector3, bool>>();


    public static GameManager getInstance()
    {
        if (_instance == null)
        {
            GameObject g = new GameObject();
            _instance = g.AddComponent<GameManager>();
        }
        return _instance;

    }

    // Returns true if the island ship is currently at has been cleared
    public bool GetCurrentIslandStatus()
    {
        Vector3 position = this.currentLocation;
        foreach (KeyValuePair<Vector3, bool> status in IslandClearedStatus)
        {
            if ((status.Key - position).magnitude <= 3)
            {
                return status.Value;
            }
        }
        // Island doesn't have a status - hasn't been cleared
        return false;
    }

    // Return the cleared status of island at position specified
    public bool GetIslandStatus(Vector3 position)
    {
        foreach (KeyValuePair<Vector3, bool> status in IslandClearedStatus)
        {
            if ((status.Key - position).magnitude <= 3)
            {
                return status.Value;
            }
        }
        // Island doesn't have a status - hasn't been cleared
        return false;
    }

    // Set the cleared status of current island to specified value
    public void SetCurrentIslandStatus(bool isCleared)
    {
        Vector3 position = this.currentLocation;

        Debug.Log("Setting island at position " + position + " to status " + isCleared);

        // Check if this island already has a key-value pair
        foreach (KeyValuePair<Vector3, bool> status in IslandClearedStatus)
        {
            if ((status.Key - position).magnitude <= 3)
            {
                IslandClearedStatus.Remove(status);
                IslandClearedStatus.Add(new KeyValuePair<Vector3, bool>(position, isCleared));
                return;
            }
        }
        if (isCleared) {
			IslandsCleared++;
            if (redRivalry + blueRivalry + whiteRivalry < 30) {
                if (islandRival == "red")
                    redRivalry++;
                else if (islandRival == "white")
                    whiteRivalry++;
                else if (islandRival == "blue")
                    blueRivalry++;
                else
                    Debug.Log("NO ISLAND");
            }
			GameManager.getInstance().notoriety = GameManager.getInstance().notoriety + 10;
			// island is clear so increase notoriety by 10 percent.
			//GameManager.getInstance ().notoriety = GameManager.getInstance ().notoriety + (int)Math.Ceiling(GameManager.getInstance ().notoriety * 0.10);
		}

        // Island doesn;t have key-value entry so add one
        IslandClearedStatus.Add(new KeyValuePair<Vector3, bool>(position, isCleared));

        //Incrementing number of islands cleared
    }

    // Return IslandController object that is at position specified
    public IslandController GetIsland(Vector3 position)
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Island"))
        {
            if ((g.transform.position - position).magnitude <= 3)
            {
                return g.GetComponent<IslandController>();
            }
        }
        Debug.Log("No island found");
        Debug.Log(position);
        return null;
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        achievements = new List<Achievement>();
        initialiseGoldAchievements();
        initialiseNoterietyAchievements();
        initialiseIslandAchievements();
        InitialiseUpgradeAchievements();
        initialiseCrew();
        InitialiseShip();
        InitialiseStats();
        crewSize = crewMembers.Count;
        crewMax = bunkCapacities[bunkLevel - 1];
    }

    void Update()
    {
        crewMax = bunkCapacities[bunkLevel - 1];
        crewSize = crewMembers.Count;
        foreach (Achievement a in achievements)
        {
            if (!a.getCompleted())
            {
                a.testAchieved(this);
            }
        }
    }
    private void initialiseNoterietyAchievements()
    {
        NotorietyAchievement n = new NotorietyAchievement("Got first notoriety", 1, "Deckhand");
        achievements.Add(n);
        n = new NotorietyAchievement("Got 50 notoriety", 50, "Scrub");
        achievements.Add(n);
        n = new NotorietyAchievement("Got 100 notoriety", 100, "Crewman");
        achievements.Add(n);
        n = new NotorietyAchievement("Got 200 notoriety", 200, "Captain");
        achievements.Add(n);
        n = new NotorietyAchievement("Got 400 notoriety", 400, "Pirate Lord");
        achievements.Add(n);
    }
    private void initialiseGoldAchievements()
    {
        GoldAchievement n = new GoldAchievement("Got 500 gold", 500, "Hobo");
        achievements.Add(n);
        n = new GoldAchievement("Got 2000 gold", 1000, "Pleb");
        achievements.Add(n);
        n = new GoldAchievement("Got 5000 gold", 2000, "MoneyBags");
        achievements.Add(n);
        n = new GoldAchievement("Got 7500 gold", 5000, "Bank");
        achievements.Add(n);
        n = new GoldAchievement("Got 10000 gold", 10000, "Hoarder");
        achievements.Add(n);
    }
    private void initialiseIslandAchievements()
    {
        IslandAchievement n = new IslandAchievement("Beat one island", 1, "Traveller");
        achievements.Add(n);
        n = new IslandAchievement("Beat 4 islands", 4, "Explorer");
        achievements.Add(n);
        n = new IslandAchievement("Beat all the islands", 10, "Harrison Jones");
        achievements.Add(n);
    }
    private void InitialiseUpgradeAchievements()
    {
        UpgradeAchievement n = new UpgradeAchievement("Upgrade all rooms to level 2", 2, "Straw House");
        achievements.Add(n);
        n = new UpgradeAchievement("Upgrade all rooms to level 3", 3, "Boat");
        achievements.Add(n);
        n = new UpgradeAchievement("Upgrade all rooms to level 4", 4, "Tank");
        achievements.Add(n);
        n = new UpgradeAchievement("Upgrade all rooms to level 5", 5, "Fortress");
        achievements.Add(n);
    }
    private void InitialiseShip()
    {
        sailsLevel = 1;
        cannonLevel = 1;
        hullLevel = 1;
        bunkLevel = 1;
    }
    private void initialiseCrew()
    {
        //Make sure to set up the reference both ways. So that equipment knows about crew, and crew knows about equipment
        crewMembers = new List<CrewMemberData>();
        armoury = new List<Armour>();
        weapons = new List<Weapon>();
        crewIndex = 0;

        CrewMemberData crew = new CrewMemberData("Luke Woly", 19, 14, 9, 100.0f, null, null);
        crew.setCrewClass(CrewMemberData.CrewClass.Bomber);
        crewMembers.Add(crew);
        Armour a = new Armour(10, "Armour 1", crew);
        Weapon w = new Weapon(30, "Weapon 1", crew);
        crew.setArmour(a);
        crew.setWeapon(w);
        armoury.Add(a);
        weapons.Add(w);


        crew = new CrewMemberData("Daniel Brocx", 12, 21, 14, 100.0f, null, null);
        crew.setCrewClass(CrewMemberData.CrewClass.Tank);
        crewMembers.Add(crew);
        a = new Armour(10, "Armour 2", crew);
        w = (new Weapon(30, "Weapon 2", crew));
        crew.setArmour(a);
        crew.setWeapon(w);
        armoury.Add(a);
        weapons.Add(w);

        armoury.Add(new Armour(8, "Armour 3", null));
        armoury.Add(new Armour(8, "Armour 4", null));

        weapons.Add(new Weapon(25, "Weapon 3", null));
        weapons.Add(new Weapon(25, "Weapon 4", null));

    }

    public void InitialiseStats()
    {
        captainName = "Cap'n BeardInProgress";
        notoriety = 0;
        gold = 500;
        IslandsCleared = 0;
        targetLocation = new Vector3(-500, -500, -500);
        currentLocation = new Vector3(-500, -500, -500);
        IslandClearedStatus = new List<KeyValuePair<Vector3, bool>>();
        redRivalry = 1;
        blueRivalry = 1;
        whiteRivalry = 1;
    }

    public void NewGame()
    {
        achievements = new List<Achievement>();
        initialiseGoldAchievements();
        initialiseNoterietyAchievements();
        initialiseIslandAchievements();
        InitialiseUpgradeAchievements();
        initialiseCrew();
        InitialiseShip();
        InitialiseStats();
        crewSize = crewMembers.Count;
        crewMax = bunkCapacities[bunkLevel - 1];
        Debug.Log("New Game");
    }
}