using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireController : MonoBehaviour {
    private Text capacityInfo;

    void Awake() {
        capacityInfo = GameObject.Find("RoomInfo").GetComponent<Text>();
        setInfoText();
    }

    void Update() {
        //Checks that there is at least 2 crew members, as you can't Fire all of your crew
        if (GameObject.Find("GameManager").GetComponent<GameManager>().crewSize == 1) {
            gameObject.GetComponent<Button>().interactable = false;
        } else {
            gameObject.GetComponent<Button>().interactable = true;
        }
        setInfoText();
    }

    public void onClickFire() {
        //Removes the current crew member from the list and resets the index.
        int index = GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex;
        int upperBound = GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.Count;

        removeEquipment(GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers[index]);
        GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.RemoveAt(GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex);
        if (index == upperBound - 1) {
            GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex = 0;
        }

    }

    private void setInfoText() {
        //Updating the information display.
        capacityInfo.text = "Level: " + GameObject.Find("GameManager").GetComponent<GameManager>().bunkLevel +
            "\nCapacity: " + GameObject.Find("GameManager").GetComponent<GameManager>().crewSize + " / " + GameObject.Find("GameManager").GetComponent<GameManager>().crewMax;
    }
    private void removeEquipment(CrewMemberData crew) {
        // Checks that a crew has any equipment, and if so removes it
        if (crew.getArmour() != null) {
            crew.getArmour().setCrewMember(null);
            crew.setArmour(null);
        }
        if (crew.getWeapon() != null) {
            crew.getWeapon().setCrewMember(null);
            crew.setWeapon(null);
        }
    }
}
