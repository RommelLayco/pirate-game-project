using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Loot : MonoBehaviour {

    public Text rewardName;
    public Text stat;
    public Text collectedGold;
    public Text char1;
    public Text char2;
    public Text char3;

    private string fullItemName = "";
    private List<string> itemNames = new List<string>();

    private bool isGold;
    private bool isSword;
    private bool isArmour;


    private int level;

    // Use this for initialization
    void Start() {
        level = GameManager.getInstance().islandLevel;
        MazeGold();
        //choose what time of items is
        ChooseItem();
		if (GameManager.getInstance ().islandLevel == 5 && Random.Range(1, 11) == 1) {

			stat.text = "You found Captain Feathersword's treasure of 2000 gold!";
			GameManager.getInstance().gold += 2000;

			DisplayExpInfo();
			return;
		}


        if (isGold) {
            displayGoldInfo();
        } else {
            InitItemNames();
            if (isSword) {
                DisplaySwordInfo();
            } else if (isArmour) {
                DisplayArmourInfo();
            }
        }

        DisplayExpInfo();
    }

    void MazeGold() {
        //display total gold collect during maze exploration
        int mGold = GameManager.getInstance().mazeGold;
        if (mGold != 0) {
            collectedGold.text = "Gold Found: " + mGold;
        }
        GameManager.getInstance().mazeGold = 0;
    }


    private void InitItemNames() {
        itemNames.Add("the OP Daniel");
        itemNames.Add("Awesomeness");
        itemNames.Add("Greatness");
        itemNames.Add("the weak Luke");
        itemNames.Add("the smelly Ben");
        itemNames.Add("Perfection");
        itemNames.Add("the lame name");
        itemNames.Add("the cool name");
    }

    //set name of item
    string ItemName(string thing) {
        //randomly choose an item name
        int index = Random.Range(0, itemNames.Count);
        string name = itemNames[index];
        string fullName = thing + " " + name;
        rewardName.text = "You found " + fullName;
        return fullName;
    }

    //choosoe item
    void ChooseItem() {
        int value = Random.Range(1, 13);

        // get a sword
        if (value < 3) {
            isSword = true;
            isArmour = false;
            isGold = false;
        } // get armour
        else if (value < 6) {
            isSword = false;
            isArmour = true;
            isGold = false;
        } else {
            isSword = false;
            isArmour = false;
            isGold = true;
        }
        //will be gold 50% of the time
    }

    void displayGoldInfo() {
        int totolGold = Random.Range(20, 51);
        totolGold = totolGold * 3;
        rewardName.text = "Loot: " + totolGold;
        //transfer collected gold
        GameManager.getInstance().gold += totolGold;
    }

    //need to check if the armoury is full for both armour info and swordinfo method
    void DisplayArmourInfo() {
        string name = ItemName("Armour of");
        int str = Random.Range(5, 15) * level;
        Armour armour = new Armour(str, name, null);
        stat.text = "Strength: " + str;
        GameManager.getInstance().armoury.Add(armour);

    }

    void DisplaySwordInfo() {
        string name = ItemName("Sword of");
        int str = Random.Range(5, 30) * level;
        Weapon weapon = new Weapon(str, name, null);
        stat.text = "Strength: " + str;
        GameManager.getInstance().weapons.Add(weapon);
    }

    void DisplayExpInfo() {
        List<CrewMemberData> explorers = GameManager.getInstance().explorers;
        Debug.Log("Exploreres Count: " + explorers.Count);
        //need to get list of crew that went exploring
        switch (explorers.Count) {
            case 1:
                ExpInfo(char2, explorers[0]);
                break;
            case 2:
                ExpInfo(char1, explorers[0]);
                ExpInfo(char3, explorers[1]);
                break;
            case 3:
                ExpInfo(char1, explorers[0]);
                ExpInfo(char2, explorers[1]);
                ExpInfo(char3, explorers[2]);
                break;
            default:
                break;
        }
    }

    void ExpInfo(Text t, CrewMemberData explorer) {
        StringBuilder expDisplay = new StringBuilder();
        expDisplay.Append(explorer.getName() + " Gained " + explorer.getXPGainedOnIsland() + " XP");
        if (explorer.getLevelsGainedOnIsland() >= 2) {
            expDisplay.Append(" \nand leveled up " + explorer.getLevelsGainedOnIsland() + " times!");
        } else if (explorer.getLevelsGainedOnIsland() == 1) {
            expDisplay.Append(" \nand leveled up once!!");
        }
        t.text = expDisplay.ToString();
        explorer.setXPGainedOnIsland(0);
        explorer.resetLevelsGainedOnIsland();
    }
}
