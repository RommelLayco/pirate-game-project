using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArmouryCrewSelector : MonoBehaviour {
    private GameManager manager;

    private Text crewInfo;
    private CrewMemberData crew;
    private Text crewName;
    private Text newText;

    void Awake() {
        manager = GameManager.getInstance();
    }

    void Start() {
        crewInfo = GameObject.Find("CrewData").GetComponent<Text>();
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
        crewInfo.text = crew.getName() + "\n" + crew.getCrewClass() + "\n" + crew.getLevel() + "\n" + crew.getAttack() + " / " + crew.getDefense() + "\n" + crew.getSpeed();
    }
}
