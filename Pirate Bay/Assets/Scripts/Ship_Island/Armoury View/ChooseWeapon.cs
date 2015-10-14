using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ChooseWeapon : MonoBehaviour {
    public Weapon weapon;

    void OnMouseDown() {
        clicked();
    }


    void clicked() {
        GameManager manager = GameManager.getInstance();
        if (manager.selectedEquipment == weapon) {
            manager.selectedEquipment = null;
        } else {
            manager.selectedEquipment = weapon;
        }
        DisplayController.setOutlines();
    }
}
