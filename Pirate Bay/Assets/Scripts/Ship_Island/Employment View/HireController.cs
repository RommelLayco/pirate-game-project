using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HireController : MonoBehaviour {
    private Text capacityInfo;
    private int crewSize;
    private GameManager manager;

    void Awake() {
        manager = GameManager.getInstance();
        capacityInfo = GameObject.Find("RoomInfo").GetComponent<Text>();
        setInfoText();
    }

    void Update() {
        //Checks that there is actually capacity for the crew member, as you can't hire more people if you dont have room.
        crewSize = manager.crewSize;
        if (crewSize >= manager.crewMax) {
                gameObject.GetComponent<Button>().interactable = false;
            } else {
            gameObject.GetComponent<Button>().interactable = true;
        }
        
        setInfoText();
    }

    public void onClickHire() {
        //Creates and adds a new crew member, then updates the index to show the newest crew member
        CrewMemberData recruited = getNewCrewMember();
        manager.crewMembers.Add(recruited);
        manager.crewIndex = manager.crewMembers.Count - 1;
    }

    private void setInfoText() {
        //Updates the information display
        capacityInfo.text = "Level: " + manager.bunkLevel + "\nCapacity: " + crewSize + " / " + manager.crewMax;
    }

    private CrewMemberData getNewCrewMember() {
        //Randomly Generates a new crew member, with randomised stats
        Random rnd = new Random();
        string name = "CrewMember #" + UnityEngine.Random.Range(1, 150);
        int attack = UnityEngine.Random.Range(3, 15);
        int defense = UnityEngine.Random.Range(1, 12);
        int speed = UnityEngine.Random.Range(1, 6);

        return new CrewMemberData(name, attack, defense, speed, null, null);
    }
}