using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddCrewToExploration : MonoBehaviour {
    private GameManager manager;
    private Text text;

    void Start() {

        manager = GameManager.getInstance();
        text = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (manager.explorers.Count == 3) {
            gameObject.GetComponent<Button>().interactable = false;
            text.text = "Dinghy to shore is full";

        }else if (manager.explorers.Count == manager.crewSize) {
            gameObject.GetComponent<Button>().interactable = false;
            text.text = "Full crew selected";
        } else {
            gameObject.GetComponent<Button>().interactable = true;
            text.text = "Take this crew member";
        }
    }

    public void onClick() {
        manager.explorers.Add(manager.crewMembers[manager.crewIndex]);
        CrewScrollerForIslandSelection.scrollLeft();    

    }

}
