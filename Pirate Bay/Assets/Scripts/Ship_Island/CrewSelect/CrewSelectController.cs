using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrewSelectController : MonoBehaviour {
    private topDownShipController shipCont;
    private GameManager manager;

    void Awake() {
        manager = GameManager.getInstance();
        shipCont = GameObject.Find("topDownShip").GetComponent<topDownShipController>();
        gameObject.GetComponent<Button>().interactable = false;

    }
    void Update() {
        if (shipAtTarget()) {
            gameObject.GetComponent<Button>().interactable = true;

        } else {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    // Opens the Sails room scene that allows upgrading of the ships cannons
    public void OnClicked() {
        Application.LoadLevel("CrewSelectionForExploration");
    }

    private bool shipAtTarget() {
        return shipCont.atTarget();
    }
}
