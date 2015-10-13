using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChooseArmour : MonoBehaviour {
    public Armour armour;

    void OnMouseDown() {
        clicked();
    }

    // save the armour to the crew member and also set the sprite
    public void clicked() {
        GameManager manager = GameManager.getInstance();
        DisplayController.setOutlines();
        if (manager.selectedEquipment == armour) {
            transform.parent.gameObject.GetComponentInChildren<OutlineController>().setSprite(OutlineController.colours.NONE);
            manager.selectedEquipment = null;
        } else {
            transform.parent.gameObject.GetComponentInChildren<OutlineController>().setSprite(OutlineController.colours.GREEN);
            manager.selectedEquipment = armour;

        }
    }
}
