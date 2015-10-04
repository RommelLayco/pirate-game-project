﻿using UnityEngine;
using System.Collections;

public abstract class Equipment : MonoBehaviour {


	private int strength;
	private string name;
	private CrewMember crewMemberAttached;

	public Equipment(int strength, string name, CrewMember crewMemberAttached){

		this.strength = strength;
		this.name = name;
		this.crewMemberAttached = crewMemberAttached;
	}


	public int getStrength(){
		return strength;
	}

	public void setStrength(int strength){
		this.strength = strength;
	}

	public string getName(){
		return name;
	}
	
	public void setName(string name){
		this.name = name;
	}


	public CrewMember getCrewMember(){
		return crewMemberAttached;
	}

	public void setCrewMember(CrewMember crewMemberAttached){
		this.crewMemberAttached = crewMemberAttached;
	}


}
