using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeCannonRoom : MonoBehaviour {
    public Transform cannon;
    public Sprite spriteG;
    public Sprite spriteS;
    public Sprite spriteB;
    private Text upgradeText;
    private Text infoText;
    private GameManager manager;

    void Awake() {
        manager = GameManager.getInstance();
        infoText = GameObject.Find("RoomInfo").GetComponent<Text>();
        upgradeText = gameObject.GetComponentInChildren<Text>();

    }

    void Start() {
        setButtonText();
        setInfoText();
        setSprite();
    }

    void Update() {
        setInfoText();

        if (manager.cannonLevel >= manager.maxLevel) {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
            //check if there is enough money to upgrade
        } else if (canAfford()) {
            gameObject.GetComponent<Button>().interactable = true;
            upgradeText.text = "Upgrade capacity from level " + manager.cannonLevel +
                "? \n$" + manager.cannonCosts[manager.cannonLevel - 1] + " gold";
        } else {
            gameObject.GetComponent<Button>().interactable = false;
            setPoorText();
        }
    }

    public void UpgradeRoom() {
        //This is only ever available to be clicked when it can be upgraded, so no checks need to be done in here
        manager.gold = manager.gold - manager.cannonCosts[manager.cannonLevel - 1];
        manager.cannonLevel++;
        setButtonText();
        manager.notoriety++;
        setInfoText();
        setSprite();

    }

    private void setInfoText() {
        //Sets the level display
        infoText.text = "Level: " + manager.cannonLevel + "/ " + manager.maxLevel;
    }
    private void setPoorText() {
        //Tells the user that they can't afford the upgrade
        upgradeText.text = "Can't afford this upgrade.\nPlease gather more gold";
    }

    private void setButtonText() {
        //Sets the button text depending on the current level
        if (manager.cannonLevel >= manager.maxLevel) {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
        } else {
            upgradeText.text = "Upgrade cannons from level " + manager.cannonLevel + "? \n$" + manager.cannonCosts[manager.cannonLevel - 1] + " gold";
        }
    }

    private bool canAfford() {
        //Checks that the upgrade can be afforded
        if (manager.cannonCosts[manager.cannonLevel - 1] <= manager.gold) {
            return true;
        }
        return false;
    }

    private void setSprite() {
        //Changing the sprite representation of the room.
        if (manager.cannonLevel < 3) {
            cannon.gameObject.GetComponent<SpriteRenderer>().sprite = spriteB;
        } else if (manager.cannonLevel < 5) {
            cannon.gameObject.GetComponent<SpriteRenderer>().sprite = spriteS;
        } else {
            cannon.gameObject.GetComponent<SpriteRenderer>().sprite = spriteG;
        }
    }
}
