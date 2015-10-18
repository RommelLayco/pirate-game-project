﻿using UnityEngine;
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

    private bool isGold = true;
    private bool isSword = false;
    private bool isArmour = false;
    

    //need to replace with level in Gamemanager
    private int level = 3;

	// Use this for initialization
	void Start ()
    {
        MazeGold();

        //choose what time of items is
        ChooseItem();

        if (isGold)
        {
            displayGoldInfo();
        }
        else
        {
            InitItemNames();
            if (isSword)
            {
                DisplaySwordInfo();
            }
            else if (isArmour)
            {
                DisplayArmourInfo();
            }
        }

        DisplayExpInfo();
	}

    void MazeGold()
    {
        //display total gold collect during maze exploration
        int mGold = GameManager.getInstance().mazeGold;
        collectedGold.text = "Gold Collected: " + mGold;
        GameManager.getInstance().mazeGold = 0;
    }


    private void InitItemNames()
    {
        itemNames.Add("the OP Daniel");
        itemNames.Add("Awesomeness");
        itemNames.Add("Greatness");
        itemNames.Add("the weak Luke");
        itemNames.Add("the smelly ben");
        itemNames.Add("Perfection");
        itemNames.Add("the lame name");
        itemNames.Add("the cool name");
    }

    //set name of item
    string ItemName(string thing)
    {
        //randomly choose an item name
        int index = Random.Range(0, itemNames.Count);
        string name = itemNames[index];
        string fullName = thing + " " + name;
        rewardName.text = "You found " + fullName;
        return fullName;
    }

    //choosoe item
    void ChooseItem()
    {
        int value = Random.Range(1, 11);

        // get a sword
        if(value < 4)
        {
            isSword = true;
            isArmour = false;
            isGold = false;
        } // get armour
        else if ( value > 3 && value < 7)
        {
            isSword = false;
            isArmour = true;
            isGold = false;
        }
        //will be gold 80% of the time
    }

    void displayGoldInfo()
    {
        int totolGold = Random.Range(20, 51);
        totolGold = totolGold * 3;
        rewardName.text = "You found " + totolGold + " Gold";
        //transfer collected gold
        GameManager.getInstance().gold += totolGold;
    }

    //need to check if the armoury is full for both armour info and swordinfo method
    void DisplayArmourInfo()
    {
        string name = ItemName("Armour of");
        int str = Random.Range(1, 11) * level;//Should factor with level as well
        Armour armour = new Armour(str, name, null);
        stat.text = "Strength: " + str;
        GameManager.getInstance().armoury.Add(armour);
        
    }

    void DisplaySwordInfo()
    {
        string name = ItemName("Sword of");
        int str = Random.Range(1, 11) * level;
        Weapon weapon = new Weapon (str, name, null);
        stat.text = "Strength: " + str;
        GameManager.getInstance().weapons.Add(weapon);
    }

    void DisplayExpInfo()
    {
        List<CrewMemberData> explorers = GameManager.getInstance().explorers; 
        Debug.Log("Exploreres Count: " + explorers.Count);
        //need to get list of crew that went exploring
        for(int i = 0; i < explorers.Count; i++)
        {
            if(i == 0)
            {
                ExpInfo(char1, explorers[i]);
            }
            else if (i == 1)
            {
                ExpInfo(char2, explorers[i]);
            }
            else if (i == 2)
            {
                ExpInfo(char3, explorers[i]);
            }
        }
    }

    void ExpInfo(Text t, CrewMemberData explorer)
    {
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
