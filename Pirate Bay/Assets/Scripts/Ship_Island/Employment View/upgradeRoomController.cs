using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class upgradeRoomController : MonoBehaviour {
    private Text upgradeText;
    private Text infoText;
    private GameManager manager;

    void Awake() {
        manager = GameManager.getInstance();
        infoText = GameObject.Find("RoomInfo").GetComponent<Text>();
    }
    void Start() {
        upgradeText = gameObject.GetComponentInChildren<Text>();
        setButtonText();
        setInfoText();
    }
    void Update() {
        //check if there is enough money to upgrade
        setButtonText();
        if (canAfford()) {
            gameObject.GetComponent<Button>().interactable = true;
            setInfoText();
        } else {
            gameObject.GetComponent<Button>().interactable = false;
            setPoorText();
        }
    }

    public void UpgradeRoom() {
        manager.gold = manager.gold - manager.bunkCosts[manager.bunkLevel - 1];
        manager.bunkLevel++;
        setButtonText();
        setInfoText();
    }

    private void setInfoText() {
        infoText.text = "Level: " + manager.bunkLevel + "\n Capacity: " + manager.crewSize + "/" + manager.bunkCapacities[manager.bunkLevel - 1];
    }
    private void setPoorText() {
        upgradeText.text = "Can't afford this upgrade.\nPlease gather more gold";
    }

    private void setButtonText() {
        if (manager.bunkLevel >= manager.maxLevel) {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
        } else {
            upgradeText.text = "Upgrade capacity from level " + manager.bunkLevel + "? \n$" + manager.bunkCosts[manager.bunkLevel - 1] + " gold";
        }
    }
    private bool canAfford() {
        if (manager.bunkCosts[manager.bunkLevel - 1] <= manager.gold) {
            return true;
        }
        return false;
    }
}
