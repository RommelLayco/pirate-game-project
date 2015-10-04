using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class upgradeRoomController : MonoBehaviour {
    Text upgradeText;
    public Text infoText;
    private int level;
    private int[] levels = { 1, 2, 3, 4, 5 };
    private int[] costs = { 100, 200, 300, 400, 500 };
    private int[] capacities = { 2, 4, 6, 8, 10 };

    void Awake() {
        level = GameObject.Find("GameManager").GetComponent<GameManager>().bunkLevel;
    }
    void Start() {
        upgradeText = gameObject.GetComponentInChildren<Text>();
        setButtonText();
        setInfoText();
    }

    public void UpgradeRoom() {
        level = level + 1;

        GameObject.Find("GameManager").GetComponent<GameManager>().bunkLevel = level;
        setButtonText();
        setInfoText();
    }

    private void setInfoText() {
        infoText.text = "Level: " + level + "\n Capacity: " + GameObject.Find("GameManager").GetComponent<GameManager>().crewSize + "/" + capacities[level - 1];
    }

    private void setButtonText() {
        if (level >= levels.Max()) {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
        } else {
            upgradeText.text = "Upgrade from level " + level + "? \n$" + costs[level - 1] + "g";
        }
    }
}
