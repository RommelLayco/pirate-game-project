using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DiscardEquipment : MonoBehaviour {
    private Button discard, equip;
    private GameManager manager;
    private DisplayController display;
    private GameObject confirmPanel;

    void Awake() {
        manager = GameManager.getInstance();
        discard = gameObject.GetComponent<Button>();
        confirmPanel = GameObject.Find("ConfirmPanel");
        disableConfirm();
    }

    void Update() {
        if (manager.selectedEquipment == null) {
            //nothing selected, so need to set both buttons inactive
            discard.interactable = false;

        } else {
            discard.interactable = true;
        }
    }

    public void onConfirmDiscard() {
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

        disableConfirm();
    }

    public void onClickDiscard() {
        //Need to make the confirmation prompt active
        enableConfirm();
    }

    public void onCancelDiscard() {
        //Need to hide the confirm prompt
        disableConfirm();

    }

    private void enableConfirm() {
        disableButtons();

        Image[] images = confirmPanel.GetComponentsInChildren<Image>();
        foreach (Image r in images) {
            r.enabled = true;
        }

        Renderer[] renderers = confirmPanel.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) {
            r.enabled = true;
        }

        Text[] texts = confirmPanel.GetComponentsInChildren<Text>();
        foreach (Text r in texts) {
            r.enabled = true;
        }
        confirmPanel.GetComponent<Image>().enabled = true;
    }

    private void disableConfirm() {
        enableButtons();

        Image[] images = confirmPanel.GetComponentsInChildren<Image>();
        foreach (Image r in images) {
            r.enabled = false;
        }

        Renderer[] renderers = confirmPanel.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) {
            r.enabled = false;
        }

        Text[] texts = confirmPanel.GetComponentsInChildren<Text>();
        foreach (Text r in texts) {
            r.enabled = false;
        }
        confirmPanel.GetComponent<Image>().enabled = false;
    }

    private void enableButtons() {
        GameObject[] blah = GameObject.FindGameObjectsWithTag("ArmourDisplay");
        foreach (GameObject g in blah) {
            g.GetComponent<Button>().interactable = true;
            //Destroy(g);
        }
        blah = GameObject.FindGameObjectsWithTag("WeaponDisplay");
        foreach (GameObject g in blah) {
            g.GetComponentInChildren<Button>().interactable = true;
            //Destroy(g);
        }
        GameObject.Find("BackButton").GetComponent<Button>().interactable = true;
    }

    private void disableButtons() {
        GameObject[] blah = GameObject.FindGameObjectsWithTag("ArmourDisplay");
        foreach (GameObject g in blah) {
            g.GetComponent<Button>().interactable = false;
            //Destroy(g);
        }
        blah = GameObject.FindGameObjectsWithTag("WeaponDisplay");
        foreach (GameObject g in blah) {
            g.GetComponentInChildren<Button>().interactable = false;
            //Destroy(g);
        }
        GameObject.Find("BackButton").GetComponent<Button>().interactable = false;
    }
}