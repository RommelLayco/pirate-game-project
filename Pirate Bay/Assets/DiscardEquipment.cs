using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DiscardEquipment : MonoBehaviour {
    private Button discard, equip;
    private GameManager manager;

    void Awake() {
        manager = GameManager.getInstance();
        discard = gameObject.GetComponent<Button>();
        equip = GameObject.Find("EquipButton").GetComponent<Button>();
    }

    void Update() {
        if (manager.selectedEquipment == null) {
            //nothing selected, so need to set both buttons inactive
            discard.interactable = false;
            equip.interactable = false;

        } else {
            discard.interactable = true;
            equip.interactable = true;
        }
    }

    public void onClickDiscard() {
        Equipment toToss = manager.selectedEquipment;
        if (toToss as Weapon != null) {
            if (toToss.getCrewMember() != null) {
                toToss.getCrewMember().setWeapon(null);
                toToss.setCrewMember(null);
            }

        } else {
            //Must be armour
            Armour a = (Armour) toToss;
            if (a.getCrewMember() != null) {
                a.getCrewMember().setArmour(null);
                a.setCrewMember(null);
            }
            Debug.Log(manager.armoury.Count);
            manager.armoury.Remove(a);
            Debug.Log(manager.armoury.Count);
        }
    }
}
