using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;

public class Loot : MonoBehaviour {

    public Text rewardName;
    public Text stat;
    public Text collectedGold;

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
        //display total gold collect during maze exploration
        collectedGold.text = "Gold Collected: " + GameManager.getInstance().mazeGold;

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

        



        
        
	}


    private void InitItemNames()
    {
        itemNames.Add("the OP Daniel");
        itemNames.Add("Awesomeness");
        itemNames.Add("Greatness");
        itemNames.Add("the weak luke");
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
        int str = Random.Range(1, 11) * level;
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

}
