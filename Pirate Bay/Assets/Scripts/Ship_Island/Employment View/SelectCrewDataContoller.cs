﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectCrewDataContoller : MonoBehaviour {
    private Text crewInfo;
    private CrewMemberData crew;
    private GameManager manager;
    private Text crewName;
    private Text newText;

    void Awake() {
        manager = GameManager.getInstance();
    }

    void Start() {
        crewInfo = GameObject.Find("CrewData").GetComponent<Text>();
        crewName = GameObject.Find("CrewName").GetComponentInChildren<InputField>().GetComponentInChildren<Text>();
        newText = GameObject.Find("InputText").GetComponent<Text>();
        setCrewInformation();
    }

    void Update() {
        setCrewInformation();
    }

    public void onLeftClick() {
        //scrolls to the crew member to the left (or the end if at the start of the list)
        manager.crewIndex--;
        if (manager.crewIndex < 0) {
            manager.crewIndex = manager.crewMembers.Count - 1;
        }
        setCrewInformation();
    }

    public void onRightClick() {
        //scrolls to the crew member to the right (or the first if at the end of the list)
        manager.crewIndex++;
        if (manager.crewIndex >= manager.crewMembers.Count) {
            manager.crewIndex = 0;
        }
        setCrewInformation();
    }

    private void setCrewInformation() {
        //Displaying the crew members name and stats
        crew = manager.crewMembers[manager.crewIndex];
        crewName.text = crew.getName();
        newText.text = crew.getName();
        crewInfo.text = "\n" + crew.getType() + "\n" + crew.getAttack() + "\n" + crew.getDefense() + "\n" + crew.getSpeed();
    }
    private void clearInput() {

        GameObject.Find("CrewName").GetComponentInChildren<InputField>().text = "";
        //newText.text = null;
    }

    public void setCrewName() {
        string inputName = newText.text;
        if (inputName.Length != 0) {
            crew.setName(inputName);
        }
        setCrewInformation();
        clearInput();
    }
}
