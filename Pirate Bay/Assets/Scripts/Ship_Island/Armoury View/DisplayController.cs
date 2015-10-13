using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DisplayController : MonoBehaviour {
    private int COLUMNS = 4;
    private int GRIDENTRIES = 12;

    public GameObject armour;

    public GameObject empty;

    public Text textPrefab;

    public GameObject weapon;

    private int defX, defY;
    private int x;
    private int y;

    private GameManager manager;

    void Awake() {
        manager = GameManager.getInstance();

    }

    // Use this for initialization
    void Start() {
        x = -200;
        y = 120;
        defX = x;
        defY = y;



    }

    public void weaponClicked() {
        // add the weapons
        for (int i = 0; i < GRIDENTRIES; i++) {
            if (i < manager.weapons.Count) {
                GameObject temp = Instantiate(weapon) as GameObject;
                Weapon localWeapon = manager.weapons[i];
                temp.GetComponentInChildren<ChooseWeapon>().weapon = localWeapon;
                temp.transform.position = new Vector3(x, y, 0);
                temp.transform.SetParent(gameObject.transform, false);

                // set the text to the value in the weapon
                temp.GetComponentInChildren<Text>().text = manager.weapons[i].getStrength().ToString();

            } else {
                //add an empty block
                addEmpty(x, y);
            }
            //Update the x and y pos
            if ((i + 1) % COLUMNS == 0) {
                x = defX;
                y = y - 100;

            } else {
                x = x + 150;
            }
        }
    }

    public void armourClicked() {
        for (int i = 0; i < GRIDENTRIES; i++) {
            if (i < manager.armoury.Count) {
                GameObject temp = Instantiate(armour) as GameObject;
                Armour localArmour = manager.armoury[i];
                temp.GetComponentInChildren<ChooseArmour>().armour = localArmour;
                temp.transform.position = new Vector3(x, y, 0);

                temp.transform.SetParent(gameObject.transform, false);
                temp.GetComponentInChildren<Text>().text = manager.armoury[i].getStrength().ToString();


            } else {
                //add an empty block
                addEmpty(x, y);
            }

            //Update the x and y pos
            if ((i + 1) % COLUMNS == 0) {
                x = defX;
                y = y - 75;

            } else {
                x = x + 150;
            }
        }
        setOutlines();

    }
    public void onClosePanel() {
        x = defX;
        y = defY;
    }
    private void addEmpty(int x, int y) {
        GameObject temp = Instantiate(empty) as GameObject;
        temp.transform.position = new Vector3(x, y, 0);
        temp.transform.SetParent(gameObject.transform, false);
    }

    public static void setOutlines() {
        GameManager manager = GameManager.getInstance();
        GameObject[] armourList = GameObject.FindGameObjectsWithTag("ArmouryDisplay");
        foreach (GameObject g in armourList) {
            Armour localEquipment = g.GetComponentInChildren<ChooseArmour>().armour;
            if (localEquipment == manager.selectedEquipment) {
                //currently equipped to this crew member --> to yellow
                g.GetComponentInChildren<OutlineController>().setSprite(OutlineController.colours.GREEN);
            } else if (localEquipment.getCrewMember() != null && localEquipment.getCrewMember() != manager.crewMembers[manager.crewIndex]) {
                //equipped to another crew member --> to red
                g.GetComponentInChildren<OutlineController>().setSprite(OutlineController.colours.RED);
            } else if (localEquipment.getCrewMember() == manager.crewMembers[manager.crewIndex]) {
                //currently equipped to this crew member --> to yellow
                g.GetComponentInChildren<OutlineController>().setSprite(OutlineController.colours.YELLOW);
            } else {
                g.GetComponentInChildren<OutlineController>().setSprite(OutlineController.colours.NONE);
            }

        }
        //Need to do the same with weapons?

    }

    public void removeImage(int x, int y) {
        //Should remove the image and create an empty one at that point
    }
}
