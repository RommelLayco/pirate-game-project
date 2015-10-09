using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeCannonRoom : MonoBehaviour {
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
		Debug.Log("gold count before= " + manager.gold);
		manager.gold = manager.gold - manager.cannonCosts[manager.cannonLevel - 1];
		Debug.Log("gold count after= " + manager.gold);
		manager.cannonLevel++;
		setButtonText();
		setInfoText();
	}


	private void setInfoText() {
		infoText.text = "Level: " + manager.cannonLevel + "/ " + manager.cannonLevels[manager.cannonLevels.Length - 1];
	}
	private void setPoorText() {
		upgradeText.text = "Can't afford this upgrade.\nPlease gather more gold";
	}
	
	private void setButtonText() {
		if (manager.cannonLevel >= manager.cannonLevels[manager.cannonLevels.Length - 1]) {
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
