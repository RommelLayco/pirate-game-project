using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrewSelectController : MonoBehaviour {
    private topDownShipController shipCont;
    private GameManager manager;
	private Color textColor;
	private GameObject panel;
	public Sprite skullSprite;
	public Sprite skullOutlineSprite;
	public Image RivalHat;

    void Awake() {
        manager = GameManager.getInstance();
        shipCont = GameObject.Find("topDownShip").GetComponent<topDownShipController>();
		textColor = gameObject.GetComponentInChildren<Text> ().color;
		panel = GameObject.Find ("BottomPanel");
		DisableButton ();
    }

    void Update() {
        if (shipAtTarget()) {
			EnableButton();
        } else {
			DisableButton();
        }
    }

	void DrawHat() {
		string rival = manager.GetIsland (manager.currentLocation).rival;
		Color color = new Color (0, 0, 0);
		if (rival == "white") {
			color = new Color (1, 1, 1);
		} else if(rival == "red") {
			color = new Color (1, 0, 0);
		} else if(rival == "blue") {
			color = new Color (0, 0, 1);
		}
		          
		RivalHat.color = color;
		RivalHat.GetComponent<CanvasRenderer> ().SetAlpha (1);

	}

	void HideHat() {
		RivalHat.GetComponent<CanvasRenderer> ().SetAlpha (0);
	}

	void ShowDifficulty() {
		// Make Skull icons visible and set them to represent the level of the island
		int level = manager.GetIsland (manager.targetLocation).level;
		for(int i = 1; i <= level; i++) {
			GameObject skull = GameObject.Find("Skull" + i);
			skull.GetComponent<Image>().sprite = skullSprite;
			skull.GetComponent<CanvasRenderer> ().SetAlpha (1);
		}

		for(int i = level + 1; i <= 5; i++) {
			GameObject skull = GameObject.Find("Skull" + i);
			skull.GetComponent<Image>().sprite = skullOutlineSprite;
			skull.GetComponent<CanvasRenderer> ().SetAlpha (1);

		}
	}

	void HideDifficulty() {
		// Hide the Skull icons
		GameObject[] skulls = GameObject.FindGameObjectsWithTag ("Skull");
		foreach (GameObject skull in skulls) {
			skull.GetComponent<CanvasRenderer> ().SetAlpha (0);
		}
	}

	void DisableButton() {
		// Disable button at bootom of screen as ship is travelling
		panel.GetComponent<CanvasRenderer> ().SetAlpha (0);
		gameObject.GetComponent<Button>().interactable = false;
		gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0);
		gameObject.GetComponentInChildren<Text>().color = Color.clear;
		HideDifficulty ();
		HideHat ();
	}

	void EnableButton() {
		// Enable the pannel at bottom of screen to allow user to explore island
		panel.GetComponent<CanvasRenderer> ().SetAlpha (1);
		gameObject.GetComponent<Button>().interactable = true;
		gameObject.GetComponent<CanvasRenderer> ().SetAlpha (1);
		gameObject.GetComponentInChildren<Text>().color = textColor;
		ShowDifficulty ();
		DrawHat ();
	}

    // Opens the menu scene that allows the user to select the crew members to take to the island
    public void OnClicked() {
        Application.LoadLevel("CrewSelectionForExploration");
    }

	// Check if the ship is at it's target
    private bool shipAtTarget() {
        return shipCont.atTarget();
    }
}
