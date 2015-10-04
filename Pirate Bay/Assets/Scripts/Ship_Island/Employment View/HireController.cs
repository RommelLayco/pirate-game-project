using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HireController : MonoBehaviour {
    private Text capacityInfo;
    private int crewSize;

    void Awake() {
        GameManager g = GameObject.Find("GameManager").GetComponent<GameManager>();
        capacityInfo = GameObject.Find("RoomInfo").GetComponent<Text>();
        setInfoText();
    }

    void Update() {
        crewSize = GameObject.Find("GameManager").GetComponent<GameManager>().crewSize;
        if (crewSize >= GameObject.Find("GameManager").GetComponent<GameManager>().crewMax) {
                gameObject.GetComponent<Button>().interactable = false;
            } else {
            gameObject.GetComponent<Button>().interactable = true;
        }
        
        setInfoText();
    }

    public void onClickHire() {
        CrewMemberData recruited = getNewCrewMember();
        GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.Add(recruited);
        //GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex = crewSize + 1;
    }

    private void setInfoText() {
        capacityInfo.text = "Capacity: " + crewSize + " / " + GameObject.Find("GameManager").GetComponent<GameManager>().crewMax;
    }

    private CrewMemberData getNewCrewMember() {
        Random rnd = new Random();
        string name = "CrewMember #" + System.DateTime.Now.Second;
        int attack = UnityEngine.Random.Range(3, 15);
        int defense = UnityEngine.Random.Range(1, 12);
        int speed = UnityEngine.Random.Range(1, 6);

        return new CrewMemberData(name, attack, defense, speed, null, null);
    }
}