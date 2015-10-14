using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeHullRoom : MonoBehaviour
{
    public Transform hull;
    public Sprite spriteL1;
    public Sprite spriteL3;
    public Sprite spriteL5;
    private Text upgradeText;
    private Text infoText;
    private GameManager manager;

    void Awake()
    {
        manager = GameManager.getInstance();
        infoText = GameObject.Find("RoomInfo").GetComponent<Text>();
    }
    void Start()
    {
        upgradeText = gameObject.GetComponentInChildren<Text>();
        setButtonText();
        setInfoText();
        setSprite();
    }

    void Update()
    {
        setInfoText();
        if (manager.hullLevel >= manager.maxLevel)
        {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
            //check if there is enough money to upgrade
        }
        else if (canAfford())
        {
            gameObject.GetComponent<Button>().interactable = true;
            upgradeText.text = "Upgrade hull from level " + manager.hullLevel
                + "? \n$" + manager.hullCosts[manager.hullLevel - 1] + " gold";
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
            setPoorText();
        }
    }

    public void UpgradeRoom()
    {
        //Upgrading the room. All checks done outside so no need to do any here
        manager.gold = manager.gold - manager.hullCosts[manager.hullLevel - 1];
        manager.hullLevel++;
        setButtonText();
        setInfoText();
        setSprite();
    }

    private void setInfoText()
    {
        infoText.text = "Level: " + manager.hullLevel + " / " + manager.maxLevel;
    }
    private void setPoorText()
    {
        upgradeText.text = "Can't afford this upgrade.\nPlease gather more gold";
    }

    private void setButtonText()
    {
        //Setting the button text depending on its current level
        if (manager.hullLevel >= manager.maxLevel)
        {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
        }
        else
        {
            upgradeText.text = "Upgrade hull from level "
                + manager.hullLevel + "? \n$" + manager.hullCosts[manager.hullLevel - 1] + " gold";

            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    private bool canAfford()
    {
        //Checks that the player can afford the next upgrade.
        if (manager.hullCosts[manager.hullLevel - 1] <= manager.gold)
        {
            return true;
        }
        return false;
    }
    private void setSprite()
    {
        if (manager.hullLevel < 3)
        {
            hull.gameObject.GetComponent<SpriteRenderer>().sprite = spriteL1;
        }
        else if (manager.hullLevel < 5)
        {
            hull.gameObject.GetComponent<SpriteRenderer>().sprite = spriteL3;
        }
        else
        {
            hull.gameObject.GetComponent<SpriteRenderer>().sprite = spriteL5;
        }
    }
}
