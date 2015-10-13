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
        GameManager.getInstance().selectedEquipment = armour;
        DisplayController.setOutlines();
        transform.parent.gameObject.GetComponentInChildren<OutlineController>().setSprite(OutlineController.colours.GREEN);
    }
}
