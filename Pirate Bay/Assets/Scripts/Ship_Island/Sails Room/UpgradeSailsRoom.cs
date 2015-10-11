using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeSailsRoom : MonoBehaviour
{
    public Transform sail;
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
    }

    void Update()
    {
        //check if there is enough money to upgrade
        setButtonText();
        if (canAfford())
        {
            gameObject.GetComponent<Button>().interactable = true;
            setInfoText();
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
            if (manager.sailsLevel<manager.maxSails) 
            setPoorText();
        }
    }

    public void UpgradeRoom()
    {
        Debug.Log("gold count before= " + manager.gold);
        manager.gold = manager.gold - manager.sailsCosts[manager.sailsLevel- 1];
        Debug.Log("gold count after= " + manager.gold);
        manager.sailsLevel++;
        setButtonText();
        setInfoText();
        if (manager.sailsLevel < 3)
        {
            sail.gameObject.GetComponent<SpriteRenderer>().sprite = spriteL1;
        }
        else if (manager.sailsLevel < 5)
        {
            sail.gameObject.GetComponent<SpriteRenderer>().sprite = spriteL3;
        }
        else
        {
            sail.gameObject.GetComponent<SpriteRenderer>().sprite = spriteL5;
        }
    }

    private void setInfoText()
    {
        infoText.text = "Level: " + manager.sailsLevel;
    }
    private void setPoorText()
    {
        upgradeText.text = "Can't afford this upgrade.\nPlease gather more gold";
    }

    private void setButtonText()
    {
        if (manager.sailsLevel >= manager.maxSails)
        {
            gameObject.GetComponent<Button>().interactable = false;
            upgradeText.text = "Fully Upgraded";
        }
        else
        {
            upgradeText.text = "Upgrade sails from level " 
                + manager.sailsLevel + "? \n$" + manager.sailsCosts[manager.sailsLevel - 1] + " gold";
        }
    }

    private bool canAfford()
    {
        if (manager.sailsCosts[manager.sailsLevel - 1] <= manager.gold)
        {
            return true;
        }
        return false;
    }
}
