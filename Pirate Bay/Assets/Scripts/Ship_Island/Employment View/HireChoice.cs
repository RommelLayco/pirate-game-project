﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;

public class HireChoice : MonoBehaviour {
    private CrewMemberData crew;
    private GameManager manager;
    private Text info;
    public Canvas popUpCanvas;
    public Canvas buttonCanvas;

    void Awake() {
        manager = GameManager.getInstance();
        crew = getNewCrewMember();
        info = gameObject.GetComponentInChildren<Text>();
        setText();
    }

    void Update() {
        setText();
    }

    public void onClick() {
        //Adds the chosen crew to the crew list
        manager.crewMembers.Add(crew);
        manager.crewIndex = manager.crewMembers.Count - 1;

		// increase notoriety by 10 % if new member is hired
		//GameManager.getInstance ().notoriety = GameManager.getInstance ().notoriety + (int)Math.Ceiling(GameManager.getInstance ().notoriety * 0.10);

        manager.notoriety = manager.notoriety + 5;
        refreshCrew();
        setText();

        //swaps the canvas back to the display
        popUpCanvas.enabled = false;
        buttonCanvas.enabled = true;
    }

    private void setText() {
        //Displaying the crew members name and stats
        info.text = "Name: " + crew.getName() + "\n Class: " + crew.getCrewClass() 
            + "\nAttack: " + crew.getAttack() + "\nDefense: " + crew.getDefense() + "\nSpeed: " + crew.getSpeed();
    }
    public void refreshCrew() {
        //need to somehow cycle the crew
        crew = getNewCrewMember();
    }

    public static CrewMemberData getNewCrewMember() {

        //Randomly Generates a new crew member, with randomised stats
        string name = "CrewMember #" + UnityEngine.Random.Range(1, 150);

        //Sets default values for neutral class type
        //int attack = UnityEngine.Random.Range(25, 45);
        //int defense = UnityEngine.Random.Range(25, 45);
        //int speed = UnityEngine.Random.Range(1, 5);
        float health = 100.0f;
        CrewMemberData recruit = new CrewMemberData(name, 0, 0, 0, health, null, null);

        int type = UnityEngine.Random.Range(1, 4);
        switch (type) {
            case 1:
                //Assassin so needs higher speed
                recruit.setAttack(UnityEngine.Random.Range(13, 18));
                recruit.setDefense(UnityEngine.Random.Range(8, 13));
                recruit.setSpeed(UnityEngine.Random.Range(18, 23));
                recruit.setCrewClass(CrewMemberData.CrewClass.Assassin);
                break;
            case 2:
                //Tank so needs higher defense
                recruit.setAttack(UnityEngine.Random.Range(8, 13));
                recruit.setDefense(UnityEngine.Random.Range(18, 23));
                recruit.setSpeed(UnityEngine.Random.Range(13, 18));
                recruit.setCrewClass(CrewMemberData.CrewClass.Tank);
                break;
            case 3:
                //Bomber needs higher attack
                recruit.setAttack(UnityEngine.Random.Range(18, 23));
                recruit.setDefense(UnityEngine.Random.Range(13, 18));
                recruit.setSpeed(UnityEngine.Random.Range(8, 13));
                recruit.setCrewClass(CrewMemberData.CrewClass.Bomber);
                break;
        }

        return recruit;
    }
}