﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectCrewDataContoller : MonoBehaviour {
    private Text crewInfo;
    private CrewMemberData crew;
    private int index;

    void Start() {
        index = GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex;
	    crewInfo = GameObject.Find("CrewData").GetComponent<Text>();
        setCrewInformation();
    }
	
	void Update () {
        GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex = index;
    }

    public void onLeftClick() {
        index--;
        if (index < 0) {
            index = GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.Count - 1;
        }
        setCrewInformation();
    }

    public void onRightClick() {
        index++;
        if (index >= GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.Count) {
            index = 0;
        }
        setCrewInformation();
    }

    private void setCrewInformation() {
        Debug.Log("index = " + index);

        crew = GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers[index];
        crewInfo.text = crew.getName() + "\n\n" + crew.getAttack() + "\n\n" + crew.getDefense() + "\n\n" + crew.getSpeed();
    }

}
