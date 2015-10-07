using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class upgradeRoomController : MonoBehaviour {
    Text upgradeText;
    private Text infoText;
    private GameManager manager;
    


    void Awake() {
        manager = GameManager.getInstance();
        Debug.Log("Level = " + manager.bunkLevel);
        Debug.Log("cost = " + manager.bunkCosts[manager.bunkLevel - 1]);
        infoText = GameObject.Find("RoomInfo").GetComponent<Text>();
    }
    void Start() {
        upgradeText = gameObject.GetComponentInChildren<Text>();
        setButtonText();
        setInfoText();
    }

    public void UpgradeRoom() {
        manager.bunkLevel++;
        setButtonText();
        setInfoText();
    }

    private void setInfoText() {
        infoText.text = "Level: " + manager.bunkLevel + "\n Capacity: " + manager.crewSize + "/" + manager.bunkCapacities[manager.bunkLevel - 1];
    }

    private void setButtonText() {
        if (manager.bunkLevel >= manager.bunkLevels.Max()) {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
        } else {
            Debug.Log("Level = " + manager.bunkLevel);
            Debug.Log("cost = " + manager.bunkCosts[manager.bunkLevel - 1]);
            upgradeText.text = "Upgrade capacity from level " + manager.bunkLevel + "? \n$" + manager.bunkCosts[manager.bunkLevel - 1] + " gold";
        }
    }
}
