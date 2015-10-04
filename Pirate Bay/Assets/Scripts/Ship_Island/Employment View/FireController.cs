using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireController : MonoBehaviour {
    private Text capacityInfo;

    void Awake() {
        capacityInfo = GameObject.Find("RoomInfo").GetComponent<Text>();
        setInfoText();
    }

    void Update() {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().crewSize == 1) {
            gameObject.GetComponent<Button>().interactable = false;
        } else {
            gameObject.GetComponent<Button>().interactable = true;
        }
        setInfoText();
    }

    public void onClickFire() {
        int index = GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex;
        int upperBound = GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.Count;
        GameObject.Find("GameManager").GetComponent<GameManager>().crewMembers.RemoveAt(GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex);
        if (index == upperBound - 1) {
            GameObject.Find("GameManager").GetComponent<GameManager>().crewIndex = 0;
        }

    }

    private void setInfoText() {
        capacityInfo.text = "Capacity: " + GameObject.Find("GameManager").GetComponent<GameManager>().crewSize + " / " + GameObject.Find("GameManager").GetComponent<GameManager>().crewMax;
    }
}
