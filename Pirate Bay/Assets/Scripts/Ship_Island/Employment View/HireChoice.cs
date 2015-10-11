using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        manager.crewMembers.Add(crew);
        manager.crewIndex = manager.crewMembers.Count - 1;
        refreshCrew();
        setText();

        popUpCanvas.enabled = false;
        buttonCanvas.enabled = true;
    }

    private void setText() {
        //Displaying the crew members name and stats
        info.text = crew.getName() + "\n Attack: " + crew.getAttack() + "\nDefense: " + crew.getDefense() + "\nSpeed: " + crew.getSpeed();
    }
    public void refreshCrew() {
        //need to somehow cycle the crew
        crew = getNewCrewMember();
    }

    public static CrewMemberData getNewCrewMember() {
        //Randomly Generates a new crew member, with randomised stats
        string name = "CrewMember #" + UnityEngine.Random.Range(1, 150);

        //Sets default values for neutral class type
        int attack = UnityEngine.Random.Range(1, 8);
        int defense = UnityEngine.Random.Range(1, 8);
        int speed = UnityEngine.Random.Range(1, 5);
        CrewMemberData recruit = new CrewMemberData(name, attack, defense, speed, null, null);

        int type = UnityEngine.Random.Range(1, 4);
        switch (type) {
            case 1:
                //Assassin so needs higher speed
                recruit.setSpeed(UnityEngine.Random.Range(3, 10));
                recruit.setType("ASSASSIN");
                break;
            case 2:
                //Tank so needs higher defense
                recruit.setDefense(UnityEngine.Random.Range(6, 15));
                recruit.setType("TANK");
                break;
            case 3:
                //Bomber needs higher attack
                recruit.setAttack(UnityEngine.Random.Range(6, 15));
                recruit.setType("BOMBER");
                break;
        }

        recruit.setXPToNext(GameManager.getInstance().levelBoundaries[0]);
        return recruit;
    }
}