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


    }
    public void onClick() {
        manager.crewMembers.Add(crew);
        manager.crewIndex = manager.crewMembers.Count - 1;
        crew = getNewCrewMember();
        setText();

        popUpCanvas.enabled = false;
        buttonCanvas.enabled = true;
        cycleOthers();
    }

    private void setText() {
        //Displaying the crew members name and stats
        info.text = crew.getName() + "\n Attack: " + crew.getAttack() + "\nDefense: " + crew.getDefense() + "\nSpeed: " + crew.getSpeed();
    }
    private void cycleOthers() {
        //need to somehow cycle the crew


    }

    public static CrewMemberData getNewCrewMember() {
        //Randomly Generates a new crew member, with randomised stats
        Random rnd = new Random();
        string name = "CrewMember #" + UnityEngine.Random.Range(1, 150);
        int attack = UnityEngine.Random.Range(3, 15);
        int defense = UnityEngine.Random.Range(1, 12);
        int speed = UnityEngine.Random.Range(1, 6);

        return new CrewMemberData(name, attack, defense, speed, null, null);
    }
}