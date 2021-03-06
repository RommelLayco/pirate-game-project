﻿using UnityEngine;
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
            crewInfo.text = crew.getName() + "\n" + crew.getCrewClass() + "\n" + crew.getLevel() + "\n" +  crew.getAttack() + "/" + crew.getDefense() + "\n" + crew.getSpeed();
        }
    }
    public static bool alreadySelectedForExploration(CrewMemberData crew) {
        //Checks that the crew members isnt already selected for exploration
        if (GameManager.getInstance().explorers.Contains(crew)) {
            return true;
        }
        return false;
    }

    public static void scrollRight() {
        //Increasing the index until a crew member is found that isn't already in the exploration list
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
        //Decreasing the index until a crew member is found that isn't already in the exploration list
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

