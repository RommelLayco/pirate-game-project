using UnityEngine;
using UnityEngine.UI;

public class upgradeRoomController : MonoBehaviour {
    Text upgradeText;
    private int level;
    private int[] levels = { 1, 2, 3, 4, 5 };
    private int[] costs = { 100, 200, 300, 400, 500 };

    void Start() {
        upgradeText = gameObject.GetComponentInChildren<Text>();
        level = 1;
        upgradeText.text = "Upgrade from level " + level + "? \n$" + costs[level -1] + "g";
    }

    public void UpgradeRoom() {
        level = (level % levels.Length) + 1;

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
