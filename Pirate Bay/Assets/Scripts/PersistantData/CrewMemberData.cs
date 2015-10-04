using UnityEngine;
using System.Collections;

public class CrewMemberData {

	private string name;
	private int attack;
	private int defense;
	private int speed;
	private Weapon weapon;
	private Armour armour;


	public CrewMemberData(string name, int attack, int defense, int speed, Weapon weapon , Armour armour){

		this.name = name;
		this.attack = attack;
		this.defense = defense;
		this.speed = speed;
		this.weapon = weapon;
		this.armour = armour;

	}

	public string getName(){
		return name;
	}

	public void setName(string name){
		this.name = name;
	}

	public int getAttack(){
		return attack;
	}
	
	public void setAttack(int attack){
		this.attack = attack;
	}


	public int getDefense(){
		return defense;
	}
	
	public void setDefense(int defense){
		this.defense = defense;
	}

	public int getSpeed(){
		return speed;
	}
	
	public void setSpeed(int speed){
		this.speed = speed;
	}

	public Weapon getWeapon(){
		return weapon;
	}
	
	public void setWeapon(Weapon weapon){
		this.weapon = weapon;
	}

	public Armour getArmour(){
		return armour;
	}
	
	public void setArmour(Armour armour){
		this.armour = armour;
	}
}
