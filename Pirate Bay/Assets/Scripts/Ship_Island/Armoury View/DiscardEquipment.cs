using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DiscardEquipment : MonoBehaviour {
    private Button discard, equip;
    private GameManager manager;

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
        Equipment toToss = manager.selectedEquipment;
        if (toToss as Weapon != null) {
            Weapon w = (Weapon)toToss;
            if (w.getCrewMember() != null) {
                w.getCrewMember().setWeapon(null);
                w.setCrewMember(null);
            }
            manager.weapons.Remove(w);

            //Need to update the GUI to remove the weapon as well
        } else {
            //Must be armour
            Armour a = (Armour) toToss;
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
