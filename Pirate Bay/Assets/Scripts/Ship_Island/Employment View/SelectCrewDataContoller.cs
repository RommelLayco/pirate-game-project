using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectCrewDataContoller : MonoBehaviour {
    private Text crewInfo;
    private static int index;
    private CrewMemberData crew;

	void Awake () {
        index = 0;
	    crewInfo = GameObject.Find("CrewData").GetComponent<Text>();
    }
	
	void Update () {
	
	}

    public void onLeftClick() {
        index--;
        Debug.Log("index = " + index);
        if (index < 0) {
            index = GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.Length -1;
        }
        Debug.Log("index After if loop = " + index);
        setCrewInformation();
    }

    public void onRightClick() {
        index++;
        Debug.Log("index = " + index);
        if (index >= GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.Length) {
            index = 0;
        }
        Debug.Log("index After if loop = " + index);
        setCrewInformation();
    }

    private void setCrewInformation() {
        crew = GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers[index];
        crewInfo.text = crew.getName() + "\n" + crew.getAttack() + "\n" + crew.getDefense() + "\n" + crew.getSpeed();
    }

}
