using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrewSelectController : MonoBehaviour {
    private topDownShipController shipCont;
    private GameManager manager;
	private Color textColor;

    void Awake() {
        manager = GameManager.getInstance();
        shipCont = GameObject.Find("topDownShip").GetComponent<topDownShipController>();
		textColor = gameObject.GetComponentInChildren<Text> ().color;
		DisableButton ();
    }

    void Update() {
        if (shipAtTarget()) {
			EnableButton();
        } else {
			DisableButton();
        }
    }

	void DisableButton() {
		gameObject.GetComponent<Button>().interactable = false;
		gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0);
		gameObject.GetComponentInChildren<Text>().color = Color.clear;
	}

	void EnableButton() {
		gameObject.GetComponent<Button>().interactable = true;
		gameObject.GetComponent<CanvasRenderer> ().SetAlpha (1);
		gameObject.GetComponentInChildren<Text>().color = textColor;

	}

    // Opens the Sails room scene that allows upgrading of the ships cannons
    public void OnClicked() {
        Application.LoadLevel("CrewSelectionForExploration");
    }

    private bool shipAtTarget() {
        return shipCont.atTarget();
    }
}
