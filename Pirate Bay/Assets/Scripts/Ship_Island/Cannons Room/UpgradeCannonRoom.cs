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
    }
    void Start() {
        upgradeText = gameObject.GetComponentInChildren<Text>();
        setButtonText();
        setInfoText();
    }

    void Update() {
        setInfoText();

        //check if there is enough money to upgrade
        if (canAfford()) {
            gameObject.GetComponent<Button>().interactable = true;
        } else {
            gameObject.GetComponent<Button>().interactable = false;
            setPoorText();
        }
        setButtonText();
    }

    public void UpgradeRoom() {
        manager.gold = manager.gold - manager.cannonCosts[manager.cannonLevel - 1];
        manager.cannonLevel++;
        setButtonText();
        setInfoText();
        if (manager.cannonLevel < 3) {
            cannon.gameObject.GetComponent<SpriteRenderer>().sprite = spriteB;
        } else if (manager.cannonLevel < 5) {
            cannon.gameObject.GetComponent<SpriteRenderer>().sprite = spriteS;
        } else {
            cannon.gameObject.GetComponent<SpriteRenderer>().sprite = spriteG;
        }
    }


    private void setInfoText() {
        infoText.text = "Level: " + manager.cannonLevel + "/ " + manager.maxLevel;
    }
    private void setPoorText() {
        upgradeText.text = "Can't afford this upgrade.\nPlease gather more gold";
    }

    private void setButtonText() {
        if (manager.cannonLevel >= manager.maxLevel) {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
        } else {
            upgradeText.text = "Upgrade cannons from level " + manager.cannonLevel + "? \n$" + manager.cannonCosts[manager.cannonLevel - 1] + " gold";
        }
    }

    private bool canAfford() {
        if (manager.cannonCosts[manager.cannonLevel - 1] <= manager.gold) {
            return true;
        }
        return false;
    }
}
