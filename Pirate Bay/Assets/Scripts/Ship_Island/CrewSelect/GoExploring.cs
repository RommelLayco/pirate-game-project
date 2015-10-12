using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoExploring : MonoBehaviour {
    private GameManager manager;
    private Button button;
    private Text information;


    void Awake() {
        manager = GameManager.getInstance();
        button = gameObject.GetComponent<Button>();
        information = button.GetComponentInChildren<Text>();

    }

    void Update() {
        //Checks that at least 1 crew member is selected to go exploring
        if (manager.explorers.Count < 1) {
            button.interactable = false;
            information.text = "Please select 1 crew member";
        } else {
            button.interactable = true;
            information.text = "Go Exploring";
        }
    }

    public void onClick() {
        //starting the island exploration.
        Application.LoadLevel("maze");
    }
}
