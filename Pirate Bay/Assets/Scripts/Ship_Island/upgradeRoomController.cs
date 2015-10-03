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
        level = (level % levels.Length) + 1;
        GameObject.Find("GameManager").GetComponent<GameManager>().bunkLevel = level;
        setButtonText();
        setInfoText();
    }

    private void setInfoText() {
        int capacity = capacities[1];
        infoText.text = "Level: " + level + "\n Capacity: " + capacity + "/" + capacities.Max();
    }

    private void setButtonText() {
        /*
     level ++;
     if (level <= levels.Length) {
         upgradeText.text = "Fully Upgraded";
         GUI.enabled = false;
     } else { */
        upgradeText.text = "Upgrade from level " + level + "? \n$" + costs[level - 1] + "g";
        //}
    }
}
