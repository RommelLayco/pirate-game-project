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
        DisplayController.setOutlines();
        if (manager.selectedEquipment == weapon) {
            transform.parent.gameObject.GetComponentInChildren<OutlineController>().setSprite(OutlineController.colours.NONE);
            manager.selectedEquipment = null;
        } else {
            transform.parent.gameObject.GetComponentInChildren<OutlineController>().setSprite(OutlineController.colours.GREEN);
            manager.selectedEquipment = weapon;

        }
    }
}
