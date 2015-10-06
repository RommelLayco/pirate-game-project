using UnityEngine;
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

    void Update() {
        setCrewInformation();
    }

    public void onLeftClick() {
        GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex--;
        if (GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex < 0) {
            GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex = GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.Count - 1;
        }
        setCrewInformation();
    }

    public void onRightClick() {
        GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex++;
        if (GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex >= GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.Count) {
            GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex = 0;
        }
        setCrewInformation();
    }

    private void setCrewInformation() {
        crew = GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers[GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex];
        crewInfo.text = crew.getName() + "\n" + crew.getAttack() + "\n" + crew.getDefense() + "\n" + crew.getSpeed();
    }

}
