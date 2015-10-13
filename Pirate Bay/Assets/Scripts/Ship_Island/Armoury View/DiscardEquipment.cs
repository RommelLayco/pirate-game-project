using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DiscardEquipment : MonoBehaviour {
    private Button discard, equip;
    private GameManager manager;
    private DisplayController display;

    void Awake() {
        manager = GameManager.getInstance();
        discard = gameObject.GetComponent<Button>();
    }

    void Update() {
        if (manager.selectedEquipment == null) {
            //nothing selected, so need to set both buttons inactive
            discard.interactable = false;

        } else {
            discard.interactable = true;
        }
    }

    public void onClickDiscard() {
       DisplayController display = GameObject.Find("SelectPanel").GetComponent<DisplayController>();



        Equipment toToss = manager.selectedEquipment;
        if (toToss as Weapon != null) {
            GameObject[] list = GameObject.FindGameObjectsWithTag("WeaponDisplay");
            Weapon w = (Weapon)toToss;
            foreach (GameObject g in list) {
                if (g.GetComponentInChildren<ChooseWeapon>().weapon == w) {
                    display.removeImage(g);
                }
            }
            if (w.getCrewMember() != null) {
                w.getCrewMember().setWeapon(null);
                w.setCrewMember(null);
            }
            manager.weapons.Remove(w);

            //Need to update the GUI to remove the weapon as well
        } else {
            //Must be armour
            GameObject[] list = GameObject.FindGameObjectsWithTag("ArmourDisplay");
            Armour a = (Armour)toToss;
            foreach (GameObject g in list) {
                if (g.GetComponentInChildren<ChooseArmour>().armour == a) {
                    display.removeImage(g);
                }
            }
            if (a.getCrewMember() != null) {
                a.getCrewMember().setArmour(null);
                a.setCrewMember(null);
            }
            manager.armoury.Remove(a);
            //Need to update the GUI to remove the armour as well
        }
        //Thinking remake panel?
        manager.selectedEquipment = null;
        DisplayController.setOutlines();
    }
}
