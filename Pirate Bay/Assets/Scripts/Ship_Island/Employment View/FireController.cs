using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireController : MonoBehaviour {
    private Text capacityInfo;
    private GameManager manager;

    void Awake() {
        manager = GameManager.getInstance();
        capacityInfo = GameObject.Find("RoomInfo").GetComponent<Text>();
        setInfoText();
    }

    void Update() {
        //Checks that there is at least 2 crew members, as you can't Fire all of your crew
        if (manager.crewSize == 1) {
            gameObject.GetComponent<Button>().interactable = false;
        } else {
            gameObject.GetComponent<Button>().interactable = true;
        }
        setInfoText();
    }

    public void onClickFire() {
        //Removes the current crew member from the list and resets the index.
        int index = manager.crewIndex;
        int upperBound = manager.crewMembers.Count;

        manager.crewMembers[index].removeEquipment();
        manager.crewMembers.RemoveAt(manager.crewIndex);

        //Update the index so that another crew member is displayed
        if (index <= upperBound - 1) {
            manager.crewIndex = 0;
        }

    }

    private void setInfoText() {
        //Updating the information display.
        capacityInfo.text = "Level: " + manager.bunkLevel +
            "\nCapacity: " + manager.crewSize + " / " + manager.crewMax;
    }
}
