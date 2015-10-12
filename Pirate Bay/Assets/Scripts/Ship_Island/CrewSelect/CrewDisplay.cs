using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CrewDisplay : MonoBehaviour {
    public int order;
    private GameManager manager;
    private Text details;

    void Awake() {
        manager = GameManager.getInstance();
        details = gameObject.GetComponentInChildren<Text>();
        manager.explorers.Clear();
    }

    void Update() {
        //Checks if the cross should be active or not
        if (manager.explorers.Count > order) {
            gameObject.GetComponent<Button>().interactable = true;
            setText();
        } else {
            gameObject.GetComponent<Button>().interactable = false;
            details.text = "";
        }
    }

    public void onClick() {
        //Removing the selected explorer from the exploration list
        manager.explorers.RemoveAt(order);
        if (CrewScrollerForIslandSelection.alreadySelectedForExploration(manager.crewMembers[manager.crewIndex])){
            CrewScrollerForIslandSelection.scrollLeft();
        }
    }

    private void setText() {
        CrewMemberData crew = manager.explorers[order];
        details.text = crew.getName();
    }
}
