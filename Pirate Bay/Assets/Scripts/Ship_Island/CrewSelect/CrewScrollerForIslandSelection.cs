using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CrewScrollerForIslandSelection : MonoBehaviour {

    private Text crewInfo;
    private CrewMemberData crew;
    private GameManager manager;

    void Awake() {
        manager = GameManager.getInstance();
    }

    void Start() {
        crewInfo = GameObject.Find("CrewData").GetComponent<Text>();
        setCrewInformation();
    }

    void Update() {
        //needs to some how scroll thru the list here to see if anyone is available
        setCrewInformation();
    }

    public void onLeftClick() {
        //scrolls to the crew member to the left (or the end if at the start of the list)
        scrollLeft();
        setCrewInformation();
    }

    public void onRightClick() {
        //scrolls to the crew member to the right (or the first if at the end of the list)
        scrollRight();
        setCrewInformation();
    }

    private void setCrewInformation() {
        //Displaying the crew members name and stats
        crew = manager.crewMembers[manager.crewIndex];
        if (alreadySelectedForExploration(crew)) {
            crewInfo.text = "All crew members \nselected for exploration";

        } else {
            crewInfo.text = crew.getName() + "\n" + crew.getAttack() + "\n" + crew.getDefense() + "\n" + crew.getSpeed();

        }
    }
    public static bool alreadySelectedForExploration(CrewMemberData crew) {
        if (GameManager.getInstance().explorers.Contains(crew)) {
            return true;
        }
        return false;
    }
    public static void scrollRight() {
        GameManager manager = GameManager.getInstance();
        manager.crewIndex++;
        if (manager.crewIndex >= manager.crewMembers.Count) {
            manager.crewIndex = 0;
        }
        if (manager.crewSize > manager.explorers.Count) {
            while (alreadySelectedForExploration(manager.crewMembers[manager.crewIndex])) {
                manager.crewIndex++;
                if (manager.crewIndex >= manager.crewMembers.Count) {
                    manager.crewIndex = 0;
                }
            }
        }
    }
    public static void scrollLeft() {
        GameManager manager = GameManager.getInstance();
        manager.crewIndex--;
        if (manager.crewIndex < 0) {
            manager.crewIndex = manager.crewMembers.Count - 1;
        }
        if (manager.crewSize > manager.explorers.Count) {
            while (CrewScrollerForIslandSelection.alreadySelectedForExploration(manager.crewMembers[manager.crewIndex])) {
                manager.crewIndex--;
                if (manager.crewIndex < 0) {
                    manager.crewIndex = manager.crewMembers.Count - 1;
                }
            }
        }
    }
}

