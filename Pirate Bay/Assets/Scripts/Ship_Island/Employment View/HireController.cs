using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HireController : MonoBehaviour {
    private Text capacityInfo;
    private int crewSize;
    private GameManager manager;
    public Canvas popUpCanvas, buttonCanvas;

    void Awake() {
        manager = GameManager.getInstance();

        buttonCanvas.enabled = true;
        popUpCanvas.enabled = false;

        capacityInfo = GameObject.Find("RoomInfo").GetComponent<Text>();
        setInfoText();
    }

    void Update() {
        //Checks that there is actually capacity for the crew member, as you can't hire more people if you dont have room.
        if (canAfford()) {
            crewSize = manager.crewSize;
            if (crewSize >= manager.crewMax) {
                gameObject.GetComponent<Button>().interactable = false;
            } else {
                gameObject.GetComponent<Button>().interactable = true;
            }
            setInfoText();
            gameObject.GetComponentInChildren<Text>().text = "Hire New Crew";
        } else {
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.GetComponentInChildren<Text>().text = "Can't afford to hire";
        }
    }

    public void onClickHire() {
        //Creates and adds a new crew member, then updates the index to show the newest crew member
        manager.gold = manager.gold - manager.hireCost;
        popUpCanvas.enabled = true;
        buttonCanvas.enabled = false;
    }
    private void setInfoText() {
        //Updates the information display
        capacityInfo.text = "Level: " + manager.bunkLevel + "\nCapacity: " + crewSize + " / " + manager.crewMax;
    }

    private bool canAfford() {
        if (manager.hireCost <= manager.gold) {
            return true;
        }
        return false;
    }
}