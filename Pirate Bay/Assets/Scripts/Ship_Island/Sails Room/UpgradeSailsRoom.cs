using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeSailsRoom : MonoBehaviour {
    public Transform sail;
    public Sprite spriteL1;
    public Sprite spriteL3;
    public Sprite spriteL5;
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
        setSprite();
    }

    void Update() {
        setInfoText();
        if (manager.sailsLevel >= manager.maxLevel) {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
            //check if there is enough money to upgrade
        } else if (canAfford()) {
            gameObject.GetComponent<Button>().interactable = true;
            upgradeText.text = "Upgrade capacity from level " + manager.sailsLevel
                + "? \n$" + manager.sailsCosts[manager.sailsLevel - 1] + " gold";
        } else {
            gameObject.GetComponent<Button>().interactable = false;
            setPoorText();
        }
    }

    public void UpgradeRoom() {
        //Upgrading the room. All checks done outside so no need to do any here
        manager.gold = manager.gold - manager.sailsCosts[manager.sailsLevel - 1];
        manager.sailsLevel++;
        setButtonText();
        setInfoText();
        setSprite();
    }

    private void setInfoText() {
        infoText.text = "Level: " + manager.sailsLevel + " / " + manager.maxLevel;
    }
    private void setPoorText() {
        upgradeText.text = "Can't afford this upgrade.\nPlease gather more gold";
    }

    private void setButtonText() {
        //Setting the button text depending on its current level
        if (manager.sailsLevel >= manager.maxLevel) {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
        } else {
            upgradeText.text = "Upgrade sails from level "
                + manager.sailsLevel + "? \n$" + manager.sailsCosts[manager.sailsLevel - 1] + " gold";

            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    private bool canAfford() {
        //Checks that the player can afford the next upgrade.
        if (manager.sailsCosts[manager.sailsLevel - 1] <= manager.gold) {
            return true;
        }
        return false;
    }
    private void setSprite() {
        if (manager.sailsLevel < 3) {
            sail.gameObject.GetComponent<SpriteRenderer>().sprite = spriteL1;
        } else if (manager.sailsLevel < 5) {
            sail.gameObject.GetComponent<SpriteRenderer>().sprite = spriteL3;
        } else {
            sail.gameObject.GetComponent<SpriteRenderer>().sprite = spriteL5;
        }
    }
}
