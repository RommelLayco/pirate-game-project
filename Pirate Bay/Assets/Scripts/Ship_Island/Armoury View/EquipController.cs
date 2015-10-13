using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipController : MonoBehaviour {
    private GameManager manager;
    private Button equip;
    private Text buttonText;
    private string equipText = "Equip to Crew Member";
    private string dequipText = "Unequip";

    void Awake() {
        manager = GameManager.getInstance();
        equip = gameObject.GetComponent<Button>();
        buttonText = equip.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (manager.selectedEquipment == null) {
            //nothing selected, so need to set both buttons inactive
            equip.interactable = false;

        } else {
            equip.interactable = true;
            if (manager.selectedEquipment.getCrewMember() == null) {
                buttonText.text = equipText;
            } else {
                buttonText.text = dequipText;
            }
        }
    }

    public void onClicked() {
        if (buttonText.text.Equals(equipText)){
            //Must be equip text
            wield();

        } else {
            //must be un equip text
            unequip();
            manager.selectedEquipment = null;
        }
        DisplayController.setOutlines();
    }

    private void wield() {
        Equipment toQuip = manager.selectedEquipment;
        CrewMemberData crew = manager.crewMembers[manager.crewIndex];
        if (toQuip as Weapon != null) {
            Weapon w = (Weapon)toQuip;
            if (crew.getWeapon() != null) {
                crew.getWeapon().setCrewMember(null);
                crew.setWeapon(null);
            }
            w.setCrewMember(crew);
            crew.setWeapon(w);
        } else {
            //Must be armour
            Armour a = (Armour)toQuip;
            if(crew.getArmour() != null) {
                crew.getArmour().setCrewMember(null);
                crew.setArmour(null);
            }
            a.setCrewMember(crew);
            crew.setArmour(a);
        }
    }

    private void unequip() {
        Equipment toToss = manager.selectedEquipment;
        if (toToss as Weapon != null) {
            Weapon w = (Weapon)toToss;
            if (w.getCrewMember() != null) {
                w.getCrewMember().setWeapon(null);
                w.setCrewMember(null);
            }
        } else {
            //Must be armour
            Armour a = (Armour)toToss;
            if (a.getCrewMember() != null) {
                a.getCrewMember().setArmour(null);
                a.setCrewMember(null);
            }
        }
        //Thinking remake panel?
        DisplayController.setOutlines();
    }
}
