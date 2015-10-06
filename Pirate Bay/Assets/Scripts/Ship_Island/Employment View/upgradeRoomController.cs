using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class upgradeRoomController : MonoBehaviour {
    Text upgradeText;
    private Text infoText;
    private int level;
    private GameManager manager;
    


    void Awake() {
        manager = GameManager.getInstance();
        level = manager.bunkLevel;
        infoText = GameObject.Find("RoomInfo").GetComponent<Text>();
    }
    void Start() {
        upgradeText = gameObject.GetComponentInChildren<Text>();
        setButtonText();
        setInfoText();
    }

    public void UpgradeRoom() {
        level = level + 1;

        manager.bunkLevel = level;
        setButtonText();
        setInfoText();
    }

    private void setInfoText() {
        infoText.text = "Level: " + level + "\n Capacity: " + manager.crewSize + "/" + manager.bunkCapacities[level - 1];
    }

    private void setButtonText() {
        if (level >= manager.bunkLevels.Max()) {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
        } else {
            upgradeText.text = "Upgrade capacity from level " + level + "? \n$" + manager.bunkCosts[level - 1] + "gold";
        }
    }
}
