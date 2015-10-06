using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectCrewDataContoller : MonoBehaviour {
    private Text crewInfo;
    private CrewMemberData crew;

    void Start() {
        crewInfo = GameObject.Find("CrewData").GetComponent<Text>();
        setCrewInformation();
    }

    void Update() {
        setCrewInformation();
    }

    public void onLeftClick() {
        //scrolls to the crew member to the left (or the end if at the start of the list)
        GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex--;
        if (GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex < 0) {
            GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex = GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.Count - 1;
        }
        setCrewInformation();
    }

    public void onRightClick() {
        //scrolls to the crew member to the right (or the first if at the end of the list)
        GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex++;
        if (GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex >= GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.Count) {
            GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex = 0;
        }
        setCrewInformation();
    }

    private void setCrewInformation() {
        //Displaying the crew members name and stats
        crew = GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers[GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex];
        crewInfo.text = crew.getName() + "\n" + crew.getAttack() + "\n" + crew.getDefense() + "\n" + crew.getSpeed();
    }

}
